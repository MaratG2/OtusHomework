using Atomic;
using UnityEngine;

namespace Homeworks5.Interfaces
{
    public interface IDirection
    {
        public AtomicVariable<Vector3> Direction { get; }
    }
}