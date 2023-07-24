using Atomic;
using System;
using UnityEngine;

namespace Homeworks5.Components
{
    public interface IRotateComponent
    {
        void Rotate(Vector3 forward);
    }

    public class RotateComponent : IRotateComponent
    {
        public IAtomicAction<Vector3> onRotate;

        public RotateComponent(IAtomicAction<Vector3> onRotate)
        {
            this.onRotate = onRotate;
        }

        void IRotateComponent.Rotate(Vector3 forward)
        {
            onRotate?.Invoke(forward);
        }
    }
}
