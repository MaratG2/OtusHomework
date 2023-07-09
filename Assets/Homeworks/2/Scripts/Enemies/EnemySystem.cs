using ShootEmUp.Enemies.Agents;
using ShootEmUp.Pool;
using UnityEngine;
using Zenject;

namespace ShootEmUp.Enemies
{
    public sealed class EnemySystem : MonoBehaviour
    {
        private PoolSettings _poolSettings;
        private EnemyPositions _enemyPositions;
        private GameObject _character;
        private PoolFacade<Enemy> _poolFacade;
        
        [Inject]
        private void Construct(PoolSettings poolSettings, EnemyPositions enemyPositions,
            MoveComponent moveComponent)
        {
            this._poolSettings = poolSettings;
            this._enemyPositions = enemyPositions;
            this._character = moveComponent.gameObject;
        }
        
        private void Awake()
        {
            _poolFacade = new (_poolSettings);
        }

        public void SpawnEnemy()
        {
            var enemy = _poolFacade.DePool();
            if (!enemy)
                enemy = _poolFacade.AddActive();
            if (!enemy)
                return;

            var spawnPosition = _enemyPositions.RandomSpawnPosition();
            enemy.transform.position = spawnPosition.position;
            var attackPosition = _enemyPositions.RandomAttackPosition();
            enemy.GetComponent<EnemyMoveAgent>().SetDestination(attackPosition.position);
            enemy.GetComponent<EnemyWeaponController>().SetTarget(_character);
        }

        public void UnspawnEnemy(GameObject enemy)
        {
            _poolFacade.EnPool(enemy.GetComponent<Enemy>());
        }
    }
}