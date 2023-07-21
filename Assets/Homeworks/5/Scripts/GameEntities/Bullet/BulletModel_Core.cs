using System;
using Atomic;
using Declarative;
using Homeworks5.Components;
using Homeworks5.Custom;
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
        public void Init(BulletModel model)
        {
            move.Init(model);
            model.onFixedUpdate += deltaTime =>
            {
                move.onMoveEvent.Invoke(deltaTime);
            };
        }
        
        [Serializable]
        public class Life
        {
            [SerializeField] private GameObject _gameObject;
            [SerializeField] public AtomicVariable<float> lifeTime;
            private Timer _timer;

            [Construct]
            public void Init(BulletModel model)
            {
                _timer = new Timer(lifeTime.Value, model);
                _timer.onEnd += () => 
                {
                    GameObject.Destroy(_gameObject);
                    _timer.Dispose();
                    _timer = null;
                };
            }
        }

        [Serializable]
        public class Bullet
        {
            [SerializeField] private Transform _transform;
            [SerializeField] public AtomicVariable<int> damage;
            [SerializeField] public CollisionObserver collisionEngine;
            
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