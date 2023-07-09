using UnityEngine;
using Zenject;

namespace ShootEmUp.Enemies
{
    public class HitPointsControllerEnemy : MonoBehaviour
    {
        private EnemySystem _enemySystem;
        private HitPointsComponent _hitPointsComponent;

        [Inject]
        private void Construct(EnemySystem enemySystem, HitPointsComponent hitPointsComponent)
        {
            this._enemySystem = enemySystem;
            this._hitPointsComponent = hitPointsComponent;
        }

        public void Reset()
        {
            _hitPointsComponent.Reset();
        }
        
        public void OnEnable()
        {
            _hitPointsComponent.OnDeath += UnspawnEnemy;
        }

        public void OnDisable()
        {
            _hitPointsComponent.OnDeath -= UnspawnEnemy;
        }

        private void UnspawnEnemy()
        {
            _enemySystem.UnspawnEnemy(gameObject);
        }
    }
}