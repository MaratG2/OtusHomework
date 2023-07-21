using System;
using Atomic;
using Declarative;
using UnityEngine;

namespace Homeworks5.Hero
{
    [Serializable]
    public class HeroModel_View
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private Transform _transform;
        [HideInInspector] public AtomicEvent<Vector3> onRotate;
        private LifeSection _life;
        private HeroModel_Core.Mover _mover;
        
        private readonly int _commonState = Animator.StringToHash("STATE");
        private readonly int _movingState = Animator.StringToHash("IS_MOVING");

        [Construct]
        public void Construct(LifeSection life, HeroModel_Core.Mover mover)
        {
            this._life = life;
            this._mover = mover;
        }

        [Construct]
        public void Init()
        {
            _animator.SetInteger(_commonState, (int)PlayerAnimationStates.Idle);
            _life.health.OnChanged += _ =>
            {
                if (!_life.isDead.Value)
                    _animator.SetInteger(_commonState, (int)PlayerAnimationStates.Hit);
            };
            _life.isDead.OnChanged += isDead =>  
            {
                if (isDead)
                    _animator.SetInteger(_commonState, (int)PlayerAnimationStates.Death);
            };
            _mover.onMove += dir =>
            {
                if (!_life.isDead.Value)
                {
                    if(dir == Vector2.zero && _animator.GetBool(_movingState))
                    {
                        _animator.SetBool(_movingState, false);
                        _animator.SetInteger(_commonState, (int)PlayerAnimationStates.Idle);
                    }
                    else
                    {
                        _animator.SetBool(_movingState, true);
                        _animator.SetInteger(_commonState, (int)PlayerAnimationStates.Run);
                    }
                }
            };
            onRotate += forward =>
            {
                if(!_life.isDead.Value)
                    _transform.rotation = Quaternion.LookRotation(forward, Vector3.up);
            };
        }
    }
}