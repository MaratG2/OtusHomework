using Homework7.Ecs.Views;
using UnityEngine;

namespace Homework7.Ecs
{
    [CreateAssetMenu(menuName = "ECS/Data/Spawn", fileName = "SpawnConfig")]
    public class SpawnSO : ScriptableObject
    {
        [SerializeField] 
        private Transform _root;
        [SerializeField]
        private int _countSpawn;
        [SerializeField]
        private EcsMonoObject _prefabCube;
        [SerializeField]
        private EcsMonoObject _prefabBullet;
        
        public Transform Root => _root;
        public int CountSpawn => _countSpawn;
        public EcsMonoObject PrefabCube => _prefabCube;
        public EcsMonoObject PrefabBullet => _prefabBullet;
    }
}