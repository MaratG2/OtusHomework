using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Homeworks.SaveLoad
{
    public sealed class SaveLoadManager : MonoBehaviour
    {
        private ISaveLoader[] _saveLoaders;
        private GameRepository _repository;
        private DiContainer _diContainer;

        [Inject]
        public void Construct(ISaveLoader[] saveLoaders, GameRepository repository,
            DiContainer diContainer)
        {
            this._saveLoaders = saveLoaders;
            this._repository = repository;
            this._diContainer = diContainer;
        }

        [Button]
        public void Load()
        {
            _repository.LoadState();
            foreach (var saveLoader in _saveLoaders)
                saveLoader.LoadGame(_repository, _diContainer);
        }

        [Button]
        private void Save()
        {
            foreach (var saveLoader in _saveLoaders)
                saveLoader.SaveGame(_repository, _diContainer);

            _repository.SaveState();
        }
        
        private void OnApplicationFocus(bool hasFocus)
        {
            if (!hasFocus)
                Save();
        }

        private void OnApplicationPause(bool pauseStatus)
        {
            if (pauseStatus)
                Save();
        }

        private void OnApplicationQuit()
        {
            Save();
        }
    }
}