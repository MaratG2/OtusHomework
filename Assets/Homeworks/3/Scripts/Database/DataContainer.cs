using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Homework3.Database
{
    [ExecuteAlways]
    public class DataContainer : SerializedMonoBehaviour
    {
        [OdinSerialize] private ISaveLoad[] _wrappers;

        private void Awake()
        {
            LoadData();
        }
        
        [Button("Load")]
        private void LoadData()
        {
            foreach (var wrapper in _wrappers)
                wrapper.Load();
        }
        
        [Button("Save")]
        private void SaveData()
        {
            foreach (var wrapper in _wrappers)
                wrapper.Save();
        }
    }
}