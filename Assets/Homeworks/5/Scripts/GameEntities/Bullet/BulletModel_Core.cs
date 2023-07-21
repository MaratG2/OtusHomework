using System;
using Atomic;
using Declarative;
using Homeworks5.Components;
using Homeworks5.Custom;
using Homeworks5.Custom.Wrappers;
using Homeworks5.Zombie;
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
        public MoveSection move = new();

        [Section] 
        [SerializeField] 
        public Bullet bullet = new();

        [Construct]
        public void Init()
        {
            move.onUpdated += deltaTime =>
            {
                move.onMoveEvent.Invoke(deltaTime);
            };
        }
        
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
                    var zombieEntity = collisionObj.gameObject.GetComponent<ZombieEntity>();
                    if (zombieEntity != null)
                    {
                        zombieEntity.Get<ITakeDamageComponent>().TakeDamage(damage.Value);
                        GameObject.Destroy(_transform.gameObject);
                    }
                };
            }
        }
    }
}