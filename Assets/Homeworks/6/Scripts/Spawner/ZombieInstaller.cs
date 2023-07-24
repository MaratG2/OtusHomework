using Homeworks5.Hero;
using Homeworks5.Zombie;
using System;
using UnityEngine;
using Zenject;

namespace Homeworks5.Spawner
{
    public class ZombieInstaller : MonoBehaviour
    {
        private GameObjectSpawner _spawner;
        private HeroEntity _heroEntity;

        [Inject]
        private void Construct(GameObjectSpawner spawner, HeroEntity heroEntity)
        {
            this._spawner = spawner;
            this._heroEntity = heroEntity;
        }

        private void OnEnable()
        {
            _spawner.onSpawned += TryToInstallZombie;
        }
        private void OnDisable()
        {
            _spawner.onSpawned -= TryToInstallZombie;
        }

        private void TryToInstallZombie(GameObject go)
        {
            if(go.TryGetComponent<ZombieModel>(out var model))
                model.Construct(_heroEntity);
        }
    }
}
