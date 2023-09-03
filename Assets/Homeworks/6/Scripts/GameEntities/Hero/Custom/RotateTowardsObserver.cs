using System;
using Atomic;
using Declarative;
using Homeworks6.Hero.States;
using UnityEngine;

namespace Homeworks6.Hero.Custom
{
    [Serializable]
    public class RotateTowardsObserver : IUpdateListener
    {
        private AtomicVariable<Entity> _target;
        private StateMachine<HeroStateType> _fsm;
        private DeclarativeModel _model;
        
        [Construct]
        private void Construct(HeroModel model)
        {
            this._model = model;
            this._fsm = model.core.heroStatesSection.stateMachine;
            this._target = model.core.target;
        }
        
        public void Update(float deltaTime)
        {
            if (_target.Value == null)
                return;
            
            if(_fsm.CurrentState == HeroStateType.Idle)
                _model.transform.LookAt(_target.Value.transform.position, Vector3.up);
        }
    }
}