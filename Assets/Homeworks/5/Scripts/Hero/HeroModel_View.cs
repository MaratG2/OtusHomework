using System;
using Atomic;
using Declarative;
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
            _life.health.OnChanged += _ =>
            {
                if (!_life.isDead.Value)
                    Debug.Log("TAKE DAMAGE ANIMATION");
            };
            _life.isDead.OnChanged += isDead =>  
            {
                if (isDead)
                    Debug.Log("DEATH ANIMATION");
            };
            _mover.onMove += _ =>
            {
                if (!_life.isDead.Value)
                    Debug.Log("MOVE ANIMATION");
            };
        }
    }
}