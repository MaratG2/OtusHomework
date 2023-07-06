using ShootEmUp.GameManagement;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public class HitPointsControllerEnemy : MonoBehaviour,
        IGameStartListener,
        IGameEndListener,
        IGameResumeListener,
        IGamePauseListener
    {
        private EnemyPool _enemyPool;
        private HitPointsComponent _hitPointsComponent;

        [Inject]
        private void Construct(EnemyPool enemyPool, HitPointsComponent hitPointsComponent)
        {
            this._enemyPool = enemyPool;
            this._hitPointsComponent = hitPointsComponent;
        }
        
        public void OnGameStart()
        {
            _hitPointsComponent.OnDeath += UnspawnEnemy;
        }

        public void OnGameEnd()
        {
            _hitPointsComponent.OnDeath -= UnspawnEnemy;
        }

        public void OnGameResume()
        {
            _hitPointsComponent.OnDeath += UnspawnEnemy;
        }

        public void OnGamePause()
        {
            _hitPointsComponent.OnDeath -= UnspawnEnemy;
        }

        private void UnspawnEnemy()
        {
            _enemyPool.UnspawnEnemy(gameObject);
        }
    }
}