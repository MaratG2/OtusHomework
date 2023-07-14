using System;
using Declarative;

namespace Homeworks5.Custom.Wrappers
{
    public class UpdateWrapper : IUpdateListener
    {
        public event Action<float> onUpdate;
        
        public void Update(float deltaTime)
        {
            onUpdate?.Invoke(deltaTime);
        }
    }
}