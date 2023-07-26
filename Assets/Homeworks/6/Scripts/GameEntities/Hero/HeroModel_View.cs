using System;
using Atomic;
using Declarative;
using Homeworks6.Hero.States;
using UnityEngine;

namespace Homeworks6.Hero
{
    [Serializable]
    public class HeroModel_View
    {
        public AnimatorStateMachine<HeroAnimationStateType> animatorMachine;
        [SerializeField] private ParticleSystem _shootVFX;
        [SerializeField] private ParticleSystem _deathVFX;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _shootSFX;
        [SerializeField] private AudioClip _deathSFX;
        [SerializeField] private Transform _transform;
        [HideInInspector] public AtomicEvent<Vector3> onRotate;
        private HeroModel_Core.HeroStates _heroStates;

        [Construct]
        public void Construct(HeroModel_Core.HeroStates heroStates)
        {
            this._heroStates = heroStates;
        }

        [Construct]
        public void Init()
        {
            onRotate += forward =>
            {
                if(_heroStates.stateMachine.CurrentState == HeroStateType.Run)
                    _transform.rotation = Quaternion.LookRotation(forward, Vector3.up);
            };
            animatorMachine.OnMessageReceived += msg =>
            {
                if (msg == "shoot")
                {
                    _shootVFX.Play();
                    _audioSource.PlayOneShot(_shootSFX);
                }
                else if (msg == "death")
                {
                    _deathVFX.Play();
                    _audioSource.PlayOneShot(_deathSFX);
                }
            };
        }

        [Construct]
        public void ConstructStates()
        {
            animatorMachine.SetupStates
            (
                (HeroAnimationStateType.Idle, null),
                (HeroAnimationStateType.Run, null),
                (HeroAnimationStateType.Shoot, null),
                (HeroAnimationStateType.Death, null)
            );
            _heroStates.stateMachine.OnStateSwitched += newState =>
            {
                animatorMachine.SwitchState((HeroAnimationStateType) (int) newState);
            };
        }
    }
}