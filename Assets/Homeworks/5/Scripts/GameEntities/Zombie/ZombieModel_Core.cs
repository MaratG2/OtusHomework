using System;
using Atomic;
using Declarative;
using Homeworks5.Components;
using Homeworks5.Custom;
using Homeworks5.Hero;
using UnityEngine;

namespace Homeworks5.Zombie
{
    [Serializable]
    public class ZombieModel_Core
    {
        [Section] 
        [SerializeField] 
        public LifeSection life = new();
        
        [Section] 
        [SerializeField] 
        public MoveSection move = new();
        
        [Section] 
        [SerializeField] 
        public Attack attack = new();
        
        [Section] 
        [SerializeField] 
        public EnemyAI enemyAI = new();

        [Construct]
        public void Init(ZombieModel model)
        {
            move.Init(model);
        }

        public void OuterInit(HeroEntity heroEntity)
        {
            life.onDeath.AddListener(() =>
            {
                heroEntity.Get<IScoresComponent>().AddScore();
                GameObject.Destroy(life.Transform.gameObject);
            });
            move.onUpdated += deltaTime =>
            {
                if (!life.isDead.Value)
                    move.onMoveEvent.Invoke(deltaTime);
            };
        }

        [Serializable]
        public class Attack
        {
            [SerializeField] private CollisionEngine _collisionEngine;
            [SerializeField] public AtomicVariable<int> damage;
            [SerializeField] public AtomicVariable<float> attackCooldown;
            private AtomicVariable<bool> _isChargingAttack = new();
            private AtomicVariable<float> _attackTimer = new();
            private HeroEntity _heroEntity;

            [Construct]
            public void Init(ZombieModel model)
            {
                _collisionEngine.onCollisionEnter += other =>
                {
                    var heroEntity = other.gameObject.GetComponent<HeroEntity>();
                    if (heroEntity != null)
                    {
                        _heroEntity = heroEntity;
                        _isChargingAttack.Value = true;
                    }
                };
                _collisionEngine.onCollisionExit += other =>
                {
                    var heroEntity = other.gameObject.GetComponent<HeroEntity>();
                    if (heroEntity != null)
                    {
                        _heroEntity = null;
                        _isChargingAttack.Value = false;
                    }
                };
                model.onUpdate += timeDelta =>
                {
                    if (_isChargingAttack.Value)
                    {
                        if (_attackTimer.Value < attackCooldown.Value)
                            _attackTimer.Value += timeDelta;
                        else
                        {
                            _attackTimer.Value -= attackCooldown.Value;
                            _heroEntity.Get<ITakeDamageComponent>().TakeDamage(damage.Value);
                        }
                    }
                    else
                        _attackTimer.Value = 0f;
                };
            }
        }

        [Serializable]
        public class EnemyAI
        {
            [SerializeField] private Transform _transform;
            [HideInInspector] public AtomicVariable<Vector2> targetDirection = new();

            [Construct]
            public void Init(ZombieModel model, MoveSection mover, LifeSection life)
            {
                model.onUpdate += _ =>
                {
                    Vector3 dir3D = model.Target.position - _transform.position;
                    targetDirection.Value = new Vector2(dir3D.x, dir3D.z);
                };
                targetDirection.OnChanged += dir =>
                {
                    if (!life.isDead.Value)
                        mover.onMove?.Invoke(dir);
                };
            }
        }
    }
}