using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

namespace Homeworks.SaveLoad
{
    public sealed class GameRepository : MonoBehaviour, IGameRepository
    {
        private const string NAME = "GameState.json";
        private const string PATH = "Assets/Homeworks/4/Resources/SaveData/";
        private string RepoPath => PATH + NAME;
        private Dictionary<string, string> _gameState = new();
        
        public void LoadState()
        {
            if (File.Exists(RepoPath))
            {
                string serializedState = "";
                using (FileStream fs = new FileStream(RepoPath, FileMode.Open))
                    using (StreamReader reader = new StreamReader(fs))
                        serializedState += reader.ReadLine();
                _gameState = JsonConvert.DeserializeObject<Dictionary<string, string>>(serializedState);
            }
            else
                _gameState = new Dictionary<string, string>();
        }

        public void SaveState()
        {
            var serializedState = JsonConvert.SerializeObject(_gameState);
            using (FileStream fs = new FileStream(RepoPath, FileMode.Create))
                using (StreamWriter writer = new StreamWriter(fs))
                    writer.Write(serializedState);
        }

        public T GetData<T>()
        {
            var serializedData = _gameState[typeof(T).Name];
            return JsonConvert.DeserializeObject<T>(serializedData);
        }

        public bool TryGetData<T>(out T value)
        {
            if (_gameState.TryGetValue(typeof(T).Name, out var serializedData))
            {
                value = JsonConvert.DeserializeObject<T>(serializedData);
                return true;
            }

            value = default;
            return false;
        }

        public void SetData<T>(T value)
        {
            var serializedData = JsonConvert.SerializeObject(value);
            _gameState[typeof(T).Name] = serializedData;
        }
    }
}