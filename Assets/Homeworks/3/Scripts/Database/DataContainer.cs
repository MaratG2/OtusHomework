using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Homework3.Database
{
    /// <summary>
    /// Only used for Save and Load for all wrappers, also in Start
    /// So you don't need to click on all of them separately
    /// Works only in Play mode
    /// </summary>
    public class DataContainer : SerializedMonoBehaviour
    {
        private ISaveLoad[] _wrappers;

        [Inject]
        private void Construct(ISaveLoad[] wrappers)
        {
            this._wrappers = wrappers;
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