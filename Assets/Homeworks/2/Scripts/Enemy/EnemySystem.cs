using System.Collections.Generic;
using ShootEmUp.Bullets;
using ShootEmUp.Pool;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public sealed class EnemySystem : MonoBehaviour
    {
        private PoolSettings _poolSettings;
        [SerializeField] private EnemyPositions enemyPositions;
        [SerializeField] private GameObject character;
        private PoolFacade<Enemy> _poolFacade;
        
        [Inject]
        private void Construct(PoolSettings poolSettings)
        {
            this._poolSettings = poolSettings;
        }
        
        private void Awake()
        {
            _poolFacade = new (_poolSettings);
        }

        public GameObject SpawnEnemy()
        {
            var enemy = _poolFacade.DePool();
            if (!enemy)
                enemy = _poolFacade.AddActive();
            if (!enemy)
                return null;

            //enemy.transform.SetParent(this.worldTransform);
            var spawnPosition = enemyPositions.RandomSpawnPosition();
            enemy.transform.position = spawnPosition.position;
            var attackPosition = enemyPositions.RandomAttackPosition();
            enemy.GetComponent<EnemyMoveAgent>().SetDestination(attackPosition.position);
            enemy.GetComponent<EnemyAttackAgent>().SetTarget(this.character);
            return enemy.gameObject;
        }

        public void UnspawnEnemy(GameObject enemy)
        {
            _poolFacade.EnPool(enemy.GetComponent<Enemy>());
        }
    }
}