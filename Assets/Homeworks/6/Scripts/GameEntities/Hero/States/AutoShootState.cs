using Atomic;
using Declarative;
using UnityEngine;

namespace Homeworks6.Hero.States
{
    public class AutoShootState : IState
    {
        private readonly DeclarativeModel _model;
        private readonly HeroModel_Core.Shoot _shoot;
        
        public AutoShootState(HeroModel_Core.Shoot shoot, HeroModel model)
        {
            _shoot = shoot;
            _model = model;
        }
        
        void IState.Enter()
        {
            _model.onLateUpdate += TryToShoot;
        }

        void IState.Exit()
        {
            _model.onLateUpdate -= TryToShoot;
        }

        private void TryToShoot(float dt)
        {
            _shoot.onShoot?.Invoke();
        }
    }
}