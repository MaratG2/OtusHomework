using System;
using System.Collections.Generic;
using Lessons.Architecture.PM;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using CharacterInfo = Lessons.Architecture.PM.CharacterInfo;

namespace Homework3.Database
{
    [RequireComponent(typeof(DataContainer))]
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
                var dataStruct = new CharacterInfoStruct();
                CharacterStatStruct[] statStructs = JsonHelper.FromJson<CharacterStatStruct>(data);
                List<CharacterStat> stats = new();
                foreach (var statStruct in statStructs)
                {
                    var newStat = new CharacterStat();
                    newStat.ChangeName(statStruct.name);
                    newStat.ChangeValue(statStruct.value);
                    stats.Add(newStat);
                }
                dataStruct.stats = stats.ToArray();
                foreach (var stat in dataStruct.stats)
                    _characterInfo.AddStat(stat);
            }
        }

        [Button("Save")]
        public void Save()
        {
            var dataStruct = new CharacterInfoStruct();
            dataStruct.stats = _characterInfo.GetStats();
            List<CharacterStatStruct> statStructs = new();
            foreach (var stat in dataStruct.stats)
                statStructs.Add(new () {name = stat.Name, value = stat.Value});
            var data = JsonHelper.ToJson(statStructs.ToArray());
            SaveLoad.Save(data, _fileName);
        }

        [Serializable]
        private struct CharacterInfoStruct
        {
            public CharacterStat[] stats;
        }

        [Serializable]
        private struct CharacterStatStruct
        {
            public string name;
            public int value;
        }
    }
}