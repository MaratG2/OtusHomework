using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Homework3.Database
{
    public class DataContainer : SerializedMonoBehaviour
    {
        private ISaveLoad[] _wrappers;
        public ISaveLoad[] Wrappers => _wrappers;

        [Inject]
        private void Construct(ISaveLoad[] wrappers)
        {
            this._wrappers = wrappers;
            Debug.Log(_wrappers.Length);
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