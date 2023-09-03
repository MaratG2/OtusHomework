using System;
using Atomic;
using Declarative;
using Homeworks6.Components;
using Homeworks6.Custom;
using Homeworks6.Zombie;
using UnityEngine;

namespace Homeworks6.Bullet
{
    [Serializable]
    public class BulletModel_Core
    {
        [Section] 
        [SerializeField] 
        public LifeTimerSection lifeTimerSection = new();
        
        [Section] 
        [SerializeField] 
        public MoveSection moveSection = new(true);

        [Section] 
        [SerializeField] 
        public CollisionDamageSection collisionDamageSection = new();

        [Construct]
        public void Init(BulletModel model)
        {
            moveSection.Init(model);
            model.onFixedUpdate += deltaTime =>
            {
                moveSection.onMoveEvent.Invoke(deltaTime);
            };
        }
        
        [Serializable]
        public class LifeTimerSection
        {
            [SerializeReference] private Timer _timer;

            [Construct]
            public void Init(BulletModel model)
            {
                _timer.Start();
                _timer.onEnd += () => GameObject.Destroy(model.gameObject);
            }
        }

        [Serializable]
        public class CollisionDamageSection
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