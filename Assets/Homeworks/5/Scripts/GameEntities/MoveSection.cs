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
        [HideInInspector] public AtomicEvent<Vector2> onMove;
        [HideInInspector] public event Action<float> onUpdated;
        [HideInInspector] public MoveEngine moveEngine = new();
        private FixedUpdateWrapper _fixedUpdate = new();

        [Construct]
        public void Init()
        {
            onMove += dir =>
            {
                moveEngine.Cache(_transform, dir, maxSpeed.Value);
            };
            _fixedUpdate.onUpdate += deltaTime =>
            {
                onUpdated?.Invoke(deltaTime);
            };
        }
    }
}
