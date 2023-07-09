using System.Collections;
using UnityEngine;
using Zenject;

namespace ShootEmUp.Enemies
{
    public sealed class EnemyManager : MonoBehaviour
    {
        private EnemySystem _enemySystem;

        [Inject]
        private void Construct(EnemySystem enemySystem)
        {
            this._enemySystem = enemySystem;
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
                _enemySystem.SpawnEnemy();
            }
        }
    }
}