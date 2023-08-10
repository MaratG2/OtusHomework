using System;
using Atomic;
using Declarative;
using Homeworks6.Components;
using Homeworks6.Custom;
using Homeworks6.Hero;
using UnityEngine;

namespace Homeworks6.Zombie
{
    [Serializable]
    public class ZombieModel_Core
    {
        [Section] 
        [SerializeField] 
        public LifeSection lifeSection = new LifeSection();
        
        [Section] 
        [SerializeField] 
        public MoveSection moveSection = new MoveSection(true);
        
        [Section] 
        [SerializeField] 
        public AttackSection attackSection = new AttackSection();
        
        [Section] 
        [SerializeField] 
        public EnemyAISection enemyAISection = new EnemyAISection();

        [Construct]
        public void Init(ZombieModel model)
        {
            moveSection.Init(model);
            model.onUpdate += deltaTime =>
            {
                if (!lifeSection.isDead.Value)
                    moveSection.onMoveEvent.Invoke(deltaTime);
            };
            lifeSection.onDeath.AddListener(() =>
            {
                GameObject.Destroy(model.gameObject);
            });
        }

        [Serializable]
        public class AttackSection
        {
            [SerializeField] private CollisionObserver _collisionEngine;
            [SerializeField] public AtomicVariable<int> damage;
            private AtomicVariable<bool> _isChargingAttack = new AtomicVariable<bool>();
            private HeroEntity _heroEntity;
            [SerializeReference] private Timer _timer;

            [Construct]
            public void Init(ZombieModel model)
            {
                _timer.Start();
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
                _isChargingAttack.OnChanged += _ => _timer.Start();
                _timer.onEnd += () =>
                {
                    if (_isChargingAttack.Value)
                    {
                        _heroEntity.Get<ITakeDamageComponent>().TakeDamage(damage.Value);
                        _timer.Start();
                    }
                };
            }
        }

        [Serializable]
        public class EnemyAISection
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