using ShootEmUp.Enemies.Agents;
using UnityEngine;
using Zenject;

namespace ShootEmUp.Enemies
{
    public class EnemySetuper
    {
        private GameObject _character;
        private EnemyPositions _enemyPositions;
        
        [Inject]
        private void Construct(EnemyPositions enemyPositions, MoveComponent moveComponent)
        {
            this._enemyPositions = enemyPositions;
            this._character = moveComponent.gameObject;
        }

        public void SetupEnemy(Enemy enemy)
        {
            var spawnPosition = _enemyPositions.RandomSpawnPosition();
            enemy.transform.position = spawnPosition.position;
            var attackPosition = _enemyPositions.RandomAttackPosition();
            enemy.GetComponent<EnemyMoveAgent>().SetDestination(attackPosition.position);
            enemy.GetComponent<EnemyWeaponController>().SetTarget(_character);
        }
    }
}