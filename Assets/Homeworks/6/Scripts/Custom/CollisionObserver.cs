using System;
using UnityEngine;

namespace Homeworks5.Custom
{
    public class CollisionObserver : MonoBehaviour
    {
        public event Action<Collision> onCollisionEnter;
        public event Action<Collision> onCollisionStay;
        public event Action<Collision> onCollisionExit;
        public void OnCollisionEnter(Collision other)
        {
            onCollisionEnter?.Invoke(other);
        }
        public void OnCollisionStay(Collision other)
        {
            onCollisionStay?.Invoke(other);
        }
        public void OnCollisionExit(Collision other)
        {
            onCollisionExit?.Invoke(other);
        }
    }
}