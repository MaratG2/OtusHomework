using System;
using Atomic;
using Declarative;
using UnityEngine;

namespace Homeworks6
{
    [Serializable]
    public class MoveEngine
    {
        [HideInInspector] public AtomicVariable<bool> IsEnabled;
        [HideInInspector] public AtomicVariable<bool> isMoving = new(false);
        [HideInInspector] public AtomicVariable<bool> wasMoving = new(false);
        [HideInInspector] public AtomicEvent<Vector2> onMove;
        [HideInInspector] public AtomicEvent<Vector3> onDirectionChange;
        [HideInInspector] public AtomicEvent<float> onMoveEvent;
        [HideInInspector] public AtomicEvent onMoveStart;
        [HideInInspector] public AtomicEvent onMoveFinish;

        public void Init(DeclarativeModel model, AtomicVariable<bool> isEnabled)
        {
            this.IsEnabled = isEnabled;
            onMove += dir =>
            {
                isMoving.Value = true;
                if(IsEnabled.Value)
                {
                    Vector3 newDir = new Vector3(dir.x, 0f, dir.y).normalized;
                    onDirectionChange?.Invoke(newDir);
                    wasMoving.Value = true;
                }
            };
           isMoving.OnUniqueChanged += moving =>
            {
                if (moving)
                    onMoveStart?.Invoke();
                else
                    onMoveFinish?.Invoke();
            };
            model.onUpdate += _ =>
            {
                if (!wasMoving.Value)
                    isMoving.Value = false;
            };
            model.onLateUpdate += _ =>
            {
                if(IsEnabled.Value)
                    wasMoving.Value = false;
            };
        }
    }
}