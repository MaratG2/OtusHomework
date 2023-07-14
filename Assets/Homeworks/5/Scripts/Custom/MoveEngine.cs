using System;
using UnityEngine;

namespace Homeworks5.Custom
{
    [Serializable]
    public class MoveEngine
    {
        private Transform _transform;
        private Vector3 _direction;
        private float _speed;

        private bool _isReady;

        public void Cache(Transform transform, Vector3 direction, float speed)
        {
            this._transform = transform;
            this._direction = direction;
            this._speed = speed;
            this._isReady = true;
        }
        
        public void Move(float deltaTime)
        {
            if (!_isReady)
                throw new NullReferenceException("MoveEngine is not ready!");
            
            _transform.position += _direction.normalized * (_speed * deltaTime);
        }
    }
}