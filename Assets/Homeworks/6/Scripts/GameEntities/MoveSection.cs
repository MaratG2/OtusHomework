using Atomic;
using Declarative;
using System;
using UnityEngine;

namespace Homeworks6
{
    [Serializable]
    public class MoveSection
    {
        public AtomicVariable<bool> IsEnabled = new();
        [SerializeField] private Transform _transform;
        [SerializeField] public AtomicVariable<float> maxSpeed;
        [HideInInspector] public AtomicVariable<Vector3> Direction;
        public MoveEngine moveEngine = new();
        
        public MoveSection(bool isEnabled)
        {
            IsEnabled.Value = isEnabled;
        }
        
        public void Init(DeclarativeModel model)
        {
            moveEngine.Init(model, IsEnabled);
            moveEngine.onDirectionChange += dir =>
            {
                Direction.Value = dir;
            };
            moveEngine.onMoveEvent.AddListener(deltaTime => 
            { 
                if(IsEnabled.Value)
                    _transform.position += Direction.Value * (maxSpeed.Value * deltaTime); 
            });
        }
    }
}
