using UnityEngine;

namespace Homeworks6.Components
{
    public interface ITransformComponent
    {
        Transform GetTransform();
    }

    public class TransformComponent : ITransformComponent
    {
        private Transform _transform;
        public TransformComponent(Transform transform)
        {
            this._transform = transform;
        }

        public Transform GetTransform()
        {
            return _transform;
        }
    }
}