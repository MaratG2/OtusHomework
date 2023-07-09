using ShootEmUp.Pool;
using UnityEngine;
using Zenject;

namespace ShootEmUp.Enemies
{
    public class Enemy : MonoBehaviour, IResetReceiver
    {
        private HitPointsControllerEnemy _hitPointsController;
        
        [Inject]
        private void Construct(HitPointsControllerEnemy hitPointsController)
        {
            this._hitPointsController = hitPointsController;
        }
        
        public void OnReset()
        {
            _hitPointsController.Reset();
        }
    }
}