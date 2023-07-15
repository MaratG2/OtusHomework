using System;
using Atomic;
using Declarative;
using Homeworks5.Custom;
using Homeworks5.Custom.Wrappers;
using Homeworks5.Interfaces;
using UnityEngine;

namespace Homeworks5.Bullet
{
    [Serializable]
    public class BulletModel_Core
    {
        [Section] 
        [SerializeField] 
        public Life life = new();
        
        [Section] 
        [SerializeField] 
        public Mover mover = new();

        [Section] 
        [SerializeField] 
        public Bullet bullet = new();
        
        [Serializable]
        public class Life
        {
            [SerializeField] private GameObject _gameObject;
            [SerializeField] public AtomicVariable<float> lifeTime;
            [HideInInspector] public AtomicVariable<float> lifeTimer;
            private UpdateWrapper _updateWrapper = new();
            
            [Construct]
            public void Init()
            {
                lifeTimer.Value = 0f;
                _updateWrapper.onUpdate += deltaTime =>
                {
                    if (lifeTimer.Value < lifeTime.Value)
                        lifeTimer.Value += deltaTime;
                    else
                        GameObject.Destroy(_gameObject);
                };
            }
        }

        [Serializable]
        public class Mover
        {
            [SerializeField] private Transform _transform;
            [SerializeField] public AtomicVariable<float> speed;
            [SerializeField] public AtomicVariable<Vector3> direction;
            private MoveEngine _moveEngine = new();
            private FixedUpdateWrapper _fixedUpdateWrapper = new();
            
            [Construct]
            public void Init()
            {
                direction.OnChanged += newDirection =>
                {
                    _moveEngine.Cache(_transform,
                        new Vector2(newDirection.x, newDirection.z),
                        speed.Value);
                };
                _fixedUpdateWrapper.onUpdate += deltaTime =>
                {
                    _moveEngine.Move(deltaTime);
                };
            }
        }

        [Serializable]
        public class Bullet
        {
            [SerializeField] private Transform _transform;
            [SerializeField] public AtomicVariable<int> damage;
            [SerializeField] public CollisionEngine collisionEngine;
            
            [Construct]
            public void Init()
            {
                collisionEngine.onCollisionEnter += collisionObj =>
                {
                    var damageable = collisionObj.gameObject.GetComponent<IDamageable>();
                    if (damageable != null)
                    {
                        damageable.TakeDamage(damage.Value);
                        GameObject.Destroy(_transform.gameObject);
                    }
                };
            }
        }
    }
}