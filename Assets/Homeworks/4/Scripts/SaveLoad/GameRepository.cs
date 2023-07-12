using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace Homeworks.SaveLoad
{
    public sealed class GameRepository : IGameRepository
    {
        private const string GAME_STATE_KEY = "Lesson/GameState";
        private Dictionary<string, string> _gameState = new();
        
        public void LoadState()
        {
            if (PlayerPrefs.HasKey(GAME_STATE_KEY))
            {
                var serializedState = PlayerPrefs.GetString(GAME_STATE_KEY);
                _gameState = JsonConvert.DeserializeObject<Dictionary<string, string>>(serializedState);
            }
            else
                _gameState = new Dictionary<string, string>();
        }

        public void SaveState()
        {
            var serializedState = JsonConvert.SerializeObject(_gameState);
            PlayerPrefs.SetString(GAME_STATE_KEY, serializedState);
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