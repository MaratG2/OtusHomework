using Atomic;
using UnityEngine;

namespace Homeworks6.Components
{
    public interface IMoveComponent
    {
        void Move(Vector2 dir);
    }

    public class MoveComponent : IMoveComponent
    {
        public IAtomicAction<Vector2> onMove;

        public MoveComponent(IAtomicAction<Vector2> onMove)
        {
            this.onMove = onMove;
        }

        void IMoveComponent.Move(Vector2 dir)
        {
            onMove?.Invoke(dir);
        }
    }
}
