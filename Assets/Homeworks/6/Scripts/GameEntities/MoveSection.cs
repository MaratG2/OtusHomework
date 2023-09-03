using Atomic;
using Declarative;
using System;
using UnityEngine;

namespace Homeworks6
{
    [Serializable]
    public class MoveSection
    {
        public bool IsEnabled;
        [SerializeField] private Transform _transform;
        [SerializeField] public AtomicVariable<float> maxSpeed;
        [HideInInspector] public AtomicVariable<Vector3> Direction;
        [HideInInspector] public AtomicEvent<Vector2> onMove;
        [HideInInspector] public AtomicEvent<float> onMoveEvent;

        private AtomicVariable<bool> _isMoving = new(false);
        [HideInInspector] public AtomicEvent onMoveStart;
        [HideInInspector] public AtomicEvent onMoveFinish;
        private bool _wasMoving = false;

        public MoveSection(bool isEnabled)
        {
            IsEnabled = isEnabled;
        }
        
        public void Init(DeclarativeModel model)
        {
            onMove += dir =>
            {
                _isMoving.Value = true;
                if(IsEnabled)
                {
                    Direction.Value = new Vector3(dir.x, 0f, dir.y).normalized;
                    _wasMoving = true;
                }
            };
            onMoveEvent.AddListener(deltaTime => 
            { 
                if(IsEnabled)
                    _transform.position += Direction.Value * (maxSpeed.Value * deltaTime); 
            });
            _isMoving.OnUniqueChanged += moving =>
            {
                if (moving)
                    onMoveStart?.Invoke();
                else
                    onMoveFinish?.Invoke();
            };
            model.onUpdate += _ =>
            {
                if (!_wasMoving)
                    _isMoving.Value = false;
            };
            model.onLateUpdate += _ =>
            {
                if(IsEnabled)
                    _wasMoving = false;
            };
        }
    }
}
