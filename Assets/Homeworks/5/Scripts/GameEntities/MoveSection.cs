using Atomic;
using Declarative;
using Homeworks5.Custom;
using Homeworks5.Custom.Wrappers;
using System;
using UnityEngine;

namespace Homeworks5
{
    [Serializable]
    public class MoveSection
    {
        [SerializeField] private Transform _transform;
        [SerializeField] public AtomicVariable<float> maxSpeed;
        [HideInInspector] public AtomicVariable<Vector3> Direction;
        [HideInInspector] public AtomicEvent<Vector2> onMove;
        [HideInInspector] public AtomicEvent<float> onMoveEvent;
        [HideInInspector] public event Action<float> onUpdated;
        private FixedUpdateWrapper _fixedUpdate = new();

        [Construct]
        public void Init()
        {
            onMove += dir =>
            {
                Direction.Value = new Vector3(dir.x, 0f, dir.y).normalized;
            };
            _fixedUpdate.onUpdate += deltaTime =>
            {
                onUpdated?.Invoke(deltaTime);
            };
            onMoveEvent.AddListener(deltaTime => 
            { 
                _transform.position += Direction.Value * (maxSpeed.Value * deltaTime); 
            });
        }
    }
}
