using Atomic;
using UnityEngine;

namespace Homeworks5.Bullet
{
    public interface IDirection
    {
        public AtomicVariable<Vector3> Direction { get; }
    }
}