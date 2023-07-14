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
        private HeroModel_Core _core;
        
        [ShowInInspector]
        public AtomicEvent<Vector3> onRotate;

        [Construct]
        public void Construct(HeroModel_Core core)
        {
            this._core = core;
        }

        [Construct]
        public void Init()
        {
            _core.onDeath += () =>  
            {
                if (_core.isDeath.Value)
                    Debug.Log("DEATH ANIMATION");
            };
        }
    }
}