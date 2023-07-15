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

        public void Cache(Transform transform, Vector2 direction, float speed)
        {
            this._transform = transform;
            this._direction = new Vector3(direction.x, 0f, direction.y).normalized;
            this._speed = speed;
            this._isReady = true;
        }
        
        public void Move(float deltaTime)
        {
            if (!_isReady)
                return;
            
            _transform.position += _direction * (_speed * deltaTime);
        }
    }
}