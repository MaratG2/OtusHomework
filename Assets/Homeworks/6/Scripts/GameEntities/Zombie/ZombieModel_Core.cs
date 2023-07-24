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
        public LifeSection life = new LifeSection();
        
        [Section] 
        [SerializeField] 
        public MoveSection move = new MoveSection();
        
        [Section] 
        [SerializeField] 
        public Attack attack = new Attack();
        
        [Section] 
        [SerializeField] 
        public EnemyAI enemyAI = new EnemyAI();

        [Construct]
        public void Init(ZombieModel model)
        {
            move.Init(model);
            model.onUpdate += deltaTime =>
            {
                if (!life.isDead.Value)
                    move.onMoveEvent.Invoke(deltaTime);
            };
        }

        public void OuterInit(HeroEntity heroEntity)
        {
            life.onDeath.AddListener(() =>
            {
                heroEntity.Get<IScoresComponent>().AddScore();
                GameObject.Destroy(life.Transform.gameObject);
            });
        }

        [Serializable]
        public class Attack
        {
            [SerializeField] private CollisionObserver _collisionEngine;
            [SerializeField] public AtomicVariable<int> damage;
            [SerializeField] public AtomicVariable<float> attackCooldown;
            private AtomicVariable<bool> _isChargingAttack = new AtomicVariable<bool>();
            private HeroEntity _heroEntity;
            private Timer _timer;

            [Construct]
            public void Init(ZombieModel model)
            {
                _timer = new Timer(attackCooldown.Value, model);
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
                _isChargingAttack.OnChanged += _ => _timer.ResetTimer();
                _timer.onEnd += () =>
                {
                    if (_isChargingAttack.Value)
                        _heroEntity.Get<ITakeDamageComponent>().TakeDamage(damage.Value);
                };
            }
        }

        [Serializable]
        public class EnemyAI
        {
            [SerializeField] private Transform _transform;
            [HideInInspector] public AtomicVariable<Vector2> targetDirection = new AtomicVariable<Vector2>();

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