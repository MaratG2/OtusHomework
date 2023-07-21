using System;
using Atomic;
using Declarative;
using Homeworks5.Custom;
using Homeworks5.Custom.Wrappers;
using Homeworks5.Interfaces;
using UnityEngine;
using Zenject;

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
        public MoveSection mover = new();
        
        [Section] 
        [SerializeField] 
        public Attacker attacker = new();
        
        [Section] 
        [SerializeField] 
        public EnemyAI enemyAI = new();

        [Construct]
        public void Init(ZombieModel model)
        {
            life.onDeath += () =>
            {
                model.scoresHolder.Kills.Value++;
                GameObject.Destroy(life.Transform.gameObject);
            };
            mover.onUpdated += deltaTime =>
            {
                if (!life.isDead.Value)
                    mover.onMoveEvent.Invoke(deltaTime);
            };
        }
        
        [Serializable]
        public class Attacker
        {
            [SerializeField] private CollisionEngine _collisionEngine;
            [SerializeField] public AtomicVariable<int> damage;
            [SerializeField] public AtomicVariable<float> attackCooldown;
            private AtomicVariable<bool> _isChargingAttack = new();
            private AtomicVariable<float> _attackTimer = new();
            private AtomicVariable<IDamageable> _player = new();
            private UpdateWrapper _updateWrapper = new();

            [Construct]
            public void Init()
            {
                _collisionEngine.onCollisionEnter += other =>
                {
                    var damageable = other.gameObject.GetComponent<IPlayerDamageable>();
                    if (damageable != null)
                    {
                        _player.Value = damageable;
                        _isChargingAttack.Value = true;
                    }
                };
                _collisionEngine.onCollisionExit += other =>
                {
                    var damageable = other.gameObject.GetComponent<IPlayerDamageable>();
                    if (damageable != null)
                    {
                        _player.Value = null;
                        _isChargingAttack.Value = false;
                    }
                };
                _updateWrapper.onUpdate += timeDelta =>
                {
                    if (_isChargingAttack.Value)
                    {
                        if (_attackTimer.Value < attackCooldown.Value)
                            _attackTimer.Value += timeDelta;
                        else
                        {
                            _attackTimer.Value -= attackCooldown.Value;
                            _player.Value.TakeDamage(damage.Value);
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
            private UpdateWrapper _updateWrapper = new();

            [Construct]
            public void Init(ZombieModel model, MoveSection mover, LifeSection life)
            {
                _updateWrapper.onUpdate += _ =>
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