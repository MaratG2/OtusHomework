using UnityEngine;
using Zenject;

namespace Homeworks.SaveLoad
{
    public abstract class SaveLoader<TData, TService> : MonoBehaviour, ISaveLoader
    where TService : MonoBehaviour
    {
        void ISaveLoader.LoadGame(IGameRepository repository, DiContainer container)
        {
            string key = gameObject.name;
            var service = container.ResolveAll<TService>()
                .Find(it => it.gameObject.name == key);
            if (repository.TryGetData(out TData data, key))
                SetupData(service, data);
            else
                SetupByDefault(service);
        }

        void ISaveLoader.SaveGame(IGameRepository repository, DiContainer container)
        {
            string key = gameObject.name;
            var service = container.ResolveAll<TService>()
                .Find(it => it.gameObject.name == key);
            var data = ConvertToData(service);
            repository.SetData(data, key);
        }

        protected abstract void SetupData(TService service, TData data);

        protected abstract TData ConvertToData(TService service);

        protected virtual void SetupByDefault(TService service)
        {
        }
    }
}