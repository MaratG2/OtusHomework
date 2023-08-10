using Atomic;
using Declarative;
using UnityEngine;

namespace Homeworks6.Hero.States
{
    public class AutoShootState : IState
    {
        private readonly DeclarativeModel _model;
        private readonly HeroModel_Core.ShootSection _shootSection;
        
        public AutoShootState(HeroModel_Core.ShootSection shootSection, HeroModel model)
        {
            _shootSection = shootSection;
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
            _shootSection.onRequestShoot?.Invoke();
        }
    }
}