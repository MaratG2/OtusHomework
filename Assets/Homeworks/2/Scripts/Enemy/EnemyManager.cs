using System.Collections;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public sealed class EnemyManager : MonoBehaviour
    {
        private EnemySystem _enemySystem;
        private BulletSystem _bulletSystem;

        [Inject]
        private void Construct(EnemySystem enemySystem, BulletSystem bulletSystem)
        {
            this._enemySystem = enemySystem;
            this._bulletSystem = bulletSystem;
        }
        
        private void Start()
        {
            StartCoroutine(SpawnAllEnemies());
        }

        private IEnumerator SpawnAllEnemies()
        {
            for (int i = 0; i < 7; i++)
            {
                yield return new WaitForSeconds(1);
                var enemy = _enemySystem.SpawnEnemy();
                if (enemy != null)
                    enemy.GetComponent<EnemyAttackAgent>().OnFire += OnFire;
            }
        }

        private void OnFire(GameObject enemy, Vector2 position, Vector2 direction)
        {
            _bulletSystem.Fire(new BulletSystem.Args
            {
                physicsLayer = (int) PhysicsLayer.ENEMY_BULLET,
                color = Color.red,
                damage = 1,
                position = position,
                velocity = direction * 2.0f
            });
        }
    }
}