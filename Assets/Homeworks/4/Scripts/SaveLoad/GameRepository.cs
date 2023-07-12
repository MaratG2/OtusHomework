using System;
using System.Collections.Generic;
using System.IO;
using EncryptionDecryptionUsingSymmetricKey;
using Newtonsoft.Json;
using UnityEngine;

namespace Homeworks.SaveLoad
{
    public sealed class GameRepository : MonoBehaviour, IGameRepository
    {
        private const string CRYPT_KEY = "b14ca5898a4e4133bbce2ea2315a1916";
        private const string NAME = "GameState.save";
        private const string PATH = "Assets/Homeworks/4/Resources/SaveData/";
        private string RepoPath => PATH + NAME;
        private Dictionary<string, string> _gameState = new();
        
        public void LoadState()
        {
            if (File.Exists(RepoPath))
            {
                byte[] bytesFile = File.ReadAllBytes(RepoPath);
                string serializedState = Convert.ToBase64String(bytesFile);
                serializedState = AesOperation.DecryptString(CRYPT_KEY, serializedState);
                _gameState = JsonConvert.DeserializeObject<Dictionary<string, string>>(serializedState);
            }
            else
                _gameState = new Dictionary<string, string>();
        }

        public void SaveState()
        {
            var serializedState = JsonConvert.SerializeObject(_gameState);
            serializedState = AesOperation.EncryptString(CRYPT_KEY, serializedState);
            byte[] bytesFile = Convert.FromBase64String(serializedState);
            File.WriteAllBytes(RepoPath, bytesFile);
        }
        public T GetData<T>(string key)
        {
            var serializedData = _gameState[typeof(T).Name+key];
            return JsonConvert.DeserializeObject<T>(serializedData);
        }

        public bool TryGetData<T>(out T value, string key)
        {
            if (_gameState.TryGetValue(typeof(T).Name+key, out var serializedData))
            {
                value = JsonConvert.DeserializeObject<T>(serializedData);
                return true;
            }

            value = default;
            return false;
        }

        public void SetData<T>(T value, string key)
        {
            var serializedData = JsonConvert.SerializeObject
                (
                    value,
                    new JsonSerializerSettings
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore  
                    }
                );
            _gameState[typeof(T).Name+key] = serializedData;
        }
    }
}