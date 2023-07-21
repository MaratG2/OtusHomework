using Homeworks5.Hero;
using Homeworks5.Interfaces;
using Homeworks5.Zombie;
using System;
using UnityEngine;
using Zenject;

namespace Homeworks5.Spawner
{
    public class ZombieInstaller : MonoBehaviour
    {
        private GameObjectSpawner _spawner;
        private HeroModel _heroModel;
        private IScores _scoresHolder;

        [Inject]
        private void Construct(GameObjectSpawner spawner, HeroModel heroModel, IScores scoresHolder)
        {
            this._spawner = spawner;
            this._heroModel = heroModel;
            this._scoresHolder = scoresHolder;
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
                model.Construct(_heroModel, _scoresHolder);
        }
    }
}
