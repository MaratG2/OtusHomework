using System;
using UnityEngine;
using Zenject;

namespace Homeworks5.Spawner
{
    public class GameObjectSpawner : MonoBehaviour
    {
        public event Action<GameObject> onSpawned;
        [SerializeField] private GameObject _prefab;
        [SerializeField] private float _spawnTime;
        private SpawnerPosition _spawnerPosition;
        private float _spawnTimer;

        [Inject]
        private void Construct(SpawnerPosition spawnerPosition)
        {
            this._spawnerPosition = spawnerPosition;
        }

        private void Update()
        {
            if (_spawnTimer < _spawnTime)
                _spawnTimer += Time.deltaTime;
            else
            {
                Spawn();
                _spawnTimer -= _spawnTime;
            }
        }

        private void Spawn()
        {
            var newGameObject = Instantiate
            (
                _prefab,
                _spawnerPosition.GetRandomPosition(),
                Quaternion.identity,
                transform
            );
            onSpawned?.Invoke(newGameObject);
        }
    }
}