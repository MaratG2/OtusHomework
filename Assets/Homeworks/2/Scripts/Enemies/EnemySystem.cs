using ShootEmUp.Enemies.Agents;
using ShootEmUp.Pool;
using UnityEngine;
using Zenject;

namespace ShootEmUp.Enemies
{
    public sealed class EnemySystem : MonoBehaviour
    {
        private EnemySetuper _enemySetuper;
        private PoolSettings _poolSettings;
        private PoolFacade<Enemy> _poolFacade;
        
        [Inject]
        private void Construct(PoolSettings poolSettings, EnemySetuper enemySetuper)
        {
            this._poolSettings = poolSettings;
            this._enemySetuper = enemySetuper;
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

            _enemySetuper.SetupEnemy(enemy);
        }

        public void UnspawnEnemy(GameObject enemy)
        {
            _poolFacade.EnPool(enemy.GetComponent<Enemy>());
        }
    }
}