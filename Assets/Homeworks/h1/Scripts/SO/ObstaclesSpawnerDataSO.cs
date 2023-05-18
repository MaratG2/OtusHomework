using UnityEngine;

namespace Homeworks.h1.SO
{
    [CreateAssetMenu(fileName = "ObstaclesSpawnerData", menuName = "SO/ObstaclesSpawenerData")]
    public class ObstaclesSpawnerDataSO : ScriptableObject
    {
        [SerializeField] private float _timeToSpawn;
        [SerializeField] private float _spawnOffset;

        public float TimeToSpawn => _timeToSpawn;
        public float SpawnOffset => _spawnOffset;
    }
}