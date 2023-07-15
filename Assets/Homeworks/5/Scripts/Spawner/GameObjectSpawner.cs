using Homeworks5.Spawner;
using UnityEngine;
using Zenject;

public class GameObjectSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private float _spawnTime;
    private SpawnerPosition _spawnerPosition;
    private DiContainer _diContainer;
    private float _spawnTimer;

    [Inject]
    private void Construct(SpawnerPosition spawnerPosition, DiContainer diContainer)
    {
        this._spawnerPosition = spawnerPosition;
        this._diContainer = diContainer;
    }
    
    private void Update()
    {
        if(_spawnTimer < _spawnTime)
            _spawnTimer += Time.deltaTime;
        else
        {
            Spawn();
            _spawnTimer -= _spawnTime;
        }
    }

    private void Spawn()
    {
        _diContainer.InstantiatePrefab
        (
            _prefab,
            _spawnerPosition.GetRandomPosition(),
            Quaternion.identity,
            transform
        );
    }
}
