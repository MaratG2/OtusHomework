using System.Collections;
using UnityEngine;
using Zenject;

namespace ShootEmUp.Enemies
{
    public sealed class EnemyManager : MonoBehaviour
    {
        [SerializeField] private int _enemyCount;
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
            for (int i = 0; i < _enemyCount; i++)
            {
                yield return new WaitForSeconds(1);
                _enemySystem.SpawnEnemy();
            }
        }
    }
}