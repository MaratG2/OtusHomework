using System;
using Declarative;

namespace Homeworks5.Custom.Wrappers
{
    public class FixedUpdateWrapper : IFixedUpdateListener
    {
        public event Action<float> onUpdate;
        
        public void FixedUpdate(float deltaTime)
        {
            onUpdate?.Invoke(deltaTime);
        }
    }
}