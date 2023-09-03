using System;
using Atomic;
using Declarative;
using Homeworks6.Hero.States;

namespace Homeworks6.Hero.Custom
{
    [Serializable]
    public class AutoShootObserver : IFixedUpdateListener
    {
        private AtomicVariable<Entity> _target;
        private StateMachine<HeroStateType> _fsm;
        private HeroModel_Core.ShootSection _shootSection;
        
        [Construct]
        private void Construct(HeroModel model)
        {
            _shootSection = model.core.shootSection;
            _fsm = model.core.heroStatesSection.stateMachine;
            _target = model.core.target;
        }

        public void FixedUpdate(float deltaTime)
        {
            if (_target.Value == null)
                return;
            
            if(_fsm.CurrentState == HeroStateType.Idle)
                _shootSection.onRequestShoot?.Invoke();
        }
    }
}