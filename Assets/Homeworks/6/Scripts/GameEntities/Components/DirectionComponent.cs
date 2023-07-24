using Atomic;
using UnityEngine;

namespace Homeworks6.Components
{
    public interface IDirectionComponent
    {
        void SetDirection(Vector3 direction);
    }

    public class DirectionComponent : IDirectionComponent
    {
        public IAtomicVariable<Vector3> direction;

        public DirectionComponent(IAtomicVariable<Vector3> direction)
        {
            this.direction = direction;
        }

        void IDirectionComponent.SetDirection(Vector3 direction)
        {
            this.direction.Value = direction;
        }
    }
}
