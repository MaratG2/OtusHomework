using Lessons.Architecture.PM;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using CharacterInfo = Lessons.Architecture.PM.CharacterInfo;

namespace Homework3.Database
{
    [RequireComponent(typeof(DataContainer))]
    [ExecuteAlways]
    public class CharacterInfoWrapper : SerializedMonoBehaviour, ISaveLoad
    {
        private readonly string _fileName = "CharacterInfo";
        [OdinSerialize] private CharacterInfo _characterInfo;
        public CharacterInfo CharacterInfo => _characterInfo;

        [Button("Load")]
        public void Load()
        {
            _characterInfo = new CharacterInfo();
            
            if(SaveLoad.HaveSave(_fileName))
            {
                string data = SaveLoad.Load(_fileName);
                CharacterInfoStruct dataStruct = JsonUtility.FromJson<CharacterInfoStruct>(data);
                if (dataStruct.stats == null)
                    return;
                foreach (var stat in dataStruct.stats)
                    _characterInfo.AddStat(stat);
            }
        }

        [Button("Save")]
        public void Save()
        {
            var dataStruct = new CharacterInfoStruct();
            dataStruct.stats = _characterInfo.GetStats();
            var data = JsonUtility.ToJson(dataStruct);
            SaveLoad.Save(data, _fileName);
        }

        private struct CharacterInfoStruct
        {
            public CharacterStat[] stats;
        }
    }
}