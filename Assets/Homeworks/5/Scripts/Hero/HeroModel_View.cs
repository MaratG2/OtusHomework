using System;
using Atomic;
using Declarative;
using Homeworks._5.Scripts.Animation;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Homeworks5.Hero
{
    [Serializable]
    public class HeroModel_View
    {
        [SerializeField] 
        private Animator _animator;
        private HeroModel_Core.Life _life;
        private HeroModel_Core.Mover _mover;
        
        private readonly int _state = Animator.StringToHash("STATE");
        
        [ShowInInspector]
        public AtomicEvent<Vector3> onRotate;

        [Construct]
        public void Construct(HeroModel_Core.Life life, HeroModel_Core.Mover mover)
        {
            this._life = life;
            this._mover = mover;
        }

        [Construct]
        public void Init()
        {
            _animator.SetInteger(_state, (int)PlayerAnimationStates.Idle);
            _life.health.OnChanged += _ =>
            {
                if (!_life.isDead.Value)
                {
                    Debug.Log("TAKE DAMAGE ANIMATION");
                    _animator.SetInteger(_state, (int)PlayerAnimationStates.Hit);
                }
            };
            _life.isDead.OnChanged += isDead =>  
            {
                if (isDead)
                {
                    Debug.Log("DEATH ANIMATION");
                    _animator.SetInteger(_state, (int)PlayerAnimationStates.Death);
                }
            };
            _mover.onMove += _ =>
            {
                if (!_life.isDead.Value)
                {
                    Debug.Log("MOVE ANIMATION");
                    _animator.SetInteger(_state, (int)PlayerAnimationStates.Run);
                }
            };
        }
    }
}