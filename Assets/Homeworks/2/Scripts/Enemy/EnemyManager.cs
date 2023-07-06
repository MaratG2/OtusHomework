using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public sealed class EnemyManager : MonoBehaviour
    {
        private EnemySystem _enemySystem;
        private BulletSystem _bulletSystem;
        private readonly HashSet<GameObject> m_activeEnemies = new();

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
                var enemy = this._enemySystem.SpawnEnemy();
                if (enemy != null)
                {
                    if (this.m_activeEnemies.Add(enemy))
                    {
                        enemy.GetComponent<HitPointsComponent>().OnDeath += this.OnDestroyed;
                        enemy.GetComponent<EnemyAttackAgent>().OnFire += this.OnFire;
                    }    
                }
            }
        }

        private void OnDestroyed()
        {
            /*
            if (m_activeEnemies.Remove(enemy))
            {
                enemy.GetComponent<HitPointsComponent>().OnDeath -= this.OnDestroyed;
                enemy.GetComponent<EnemyAttackAgent>().OnFire -= this.OnFire;

                _enemyPool.UnspawnEnemy(enemy);
            }
            */
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