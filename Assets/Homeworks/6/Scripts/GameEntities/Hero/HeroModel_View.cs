using System;
using Atomic;
using Declarative;
using Homeworks6.Custom;
using Homeworks6.Hero.Custom;
using Homeworks6.Hero.States;
using UnityEngine;

namespace Homeworks6.Hero
{
    [Serializable]
    public class HeroModel_View
    {
        public AnimatorStateMachine<HeroAnimationStateType> animatorMachine;
        [SerializeReference] private IFX _deathFX;
        [SerializeReference] private IFX _shootFX;
        [SerializeField] private Transform _transform;
        [HideInInspector] public AtomicEvent<Vector3> onRotate;
        private HeroModel_Core.HeroStatesSection _heroStatesSection;
        
        [Section] 
        public RotateTowardsObserver _rotateTowardsObserver = new();

        [Construct]
        public void Construct(HeroModel_Core.HeroStatesSection heroStatesSection)
        {
            this._heroStatesSection = heroStatesSection;
        }

        [Construct]
        public void Init()
        {
            onRotate += forward =>
            {
                if(_heroStatesSection.stateMachine.CurrentState == HeroStateType.Run)
                    _transform.rotation = Quaternion.LookRotation(forward, Vector3.up);
            };
            animatorMachine.OnMessageReceived += msg =>
            {
                if (msg == "shoot")
                    _shootFX.Play();
                else if (msg == "death")
                    _deathFX.Play();
            };
        }

        [Construct]
        public void ConstructStates()
        {
            animatorMachine.SetupStates
            (
                (HeroAnimationStateType.Shoot, null),
                (HeroAnimationStateType.Run, null),
                (HeroAnimationStateType.Death, null)
            );
            _heroStatesSection.stateMachine.OnStateSwitched += newState =>
            {
                animatorMachine.SwitchState((HeroAnimationStateType) (int) newState);
            };
        }
    }
}