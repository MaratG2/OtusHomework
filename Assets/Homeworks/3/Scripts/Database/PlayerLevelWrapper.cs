using Lessons.Architecture.PM;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Homework3.Database
{
    [RequireComponent(typeof(DataContainer))]
    public class PlayerLevelWrapper : SerializedMonoBehaviour, ISaveLoad
    {
        private readonly string _fileName = "PlayerLevel";
        [OdinSerialize] private PlayerLevel _playerLevel;
        public PlayerLevel PlayerLevel => _playerLevel;

        [Button("Load")]
        public void Load()
        {
            _playerLevel = new PlayerLevel();
            
            if(SaveLoad.HaveSave(_fileName))
            {
                string data = SaveLoad.Load(_fileName);
                PlayerLevelStruct dataStruct = JsonUtility.FromJson<PlayerLevelStruct>(data);
                for (int i = 1; i < dataStruct.level; i++)
                {
                    _playerLevel.AddExperience(_playerLevel.RequiredExperience);
                    _playerLevel.LevelUp();
                }
                _playerLevel.AddExperience(dataStruct.exp);
            }
        }

        [Button("Save")]
        public void Save()
        {
            var dataStruct = new PlayerLevelStruct();
            dataStruct.level = _playerLevel.CurrentLevel;
            dataStruct.exp = _playerLevel.CurrentExperience;
            var data = JsonUtility.ToJson(dataStruct);
            SaveLoad.Save(data, _fileName);
        }

        private struct PlayerLevelStruct
        {
            public int level;
            public int exp;
        }
    }
}