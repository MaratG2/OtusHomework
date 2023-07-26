using System;
using Atomic;
using Declarative;
using Homeworks6.Custom;
using Homeworks6.Hero.States;
using UnityEngine;

namespace Homeworks6.Hero
{
    [Serializable]
    public class HeroModel_Core
    {
        [Section]
        public LifeSection life = new LifeSection();

        [Section]
        public Shoot shoot = new Shoot();

        [Section]
        public ShootReload shootReload = new ShootReload();

        [Section]
        public MoveSection move = new MoveSection(false);
        [HideInInspector] public AtomicVariable<bool> moveRequired;
        
        [Section]
        public HeroStates heroStates = new HeroStates();

        [Construct]
        public void Init(HeroModel model)
        {
            move.Init(model);

            life.onDeath += () =>
            {
                Debug.Log("ИГРА ЗАВЕРШЕНА");
            };

            move.onMove += dir =>
            {
                moveRequired.Value = true;
            };
            model.onFixedUpdate += deltaTime =>
            {
                if (moveRequired.Value)
                {
                    move.onMoveEvent.Invoke(deltaTime);
                    moveRequired.Value = false;
                }
            };
        }

        [Serializable]
        public class Shoot
        {
            [SerializeField] private Transform _shootPoint;
            [SerializeField] private GameObject _bulletPrefab;
            [SerializeField] public AtomicVariable<float> cooldownTime;
            [HideInInspector] public AtomicVariable<int> kills;  
            [HideInInspector] public AtomicVariable<bool> isReadyShoot;
            [HideInInspector] public AtomicEvent onShoot;
            [HideInInspector] public AtomicEvent onShootPerformed;
            [HideInInspector] public AtomicEvent onKilled;
            private BulletShooter _shooter = new BulletShooter();
            private Timer _timer;

            [Construct]
            public void Init(LifeSection life, ShootReload shootReloader, HeroModel model)
            {
                _timer = new Timer(cooldownTime.Value, model, true);
                _timer.onEnd += () => isReadyShoot.Value = true;
                onShoot.AddListener(() =>
                {
                    if (shootReloader.hasBullets.Value && isReadyShoot.Value && !life.isDead.Value)
                    {
                        isReadyShoot.Value = false;
                        _timer.ResetTimer();
                        _shooter.Shoot(_bulletPrefab, _shootPoint, _shootPoint.forward);
                        onShootPerformed?.Invoke();
                    }
                });
                onKilled += () =>
                {
                    kills.Value++;
                };
            }
        }

        [Serializable]
        public class ShootReload
        {
            [SerializeField] public AtomicVariable<int> maxBullets;
            [SerializeField] public AtomicVariable<float> bulletRestoreCooldown;
            [HideInInspector] public AtomicVariable<int> currentBullets;
            [HideInInspector] public AtomicVariable<bool> hasBullets;
            [HideInInspector] public AtomicEvent onReload;
            
            private AutoTimer _timer;

            [Construct]
            public void Init(Shoot shooter, HeroModel model)
            {
                _timer = new AutoTimer(bulletRestoreCooldown.Value, model);
                currentBullets.OnChanged += newBullets =>
                {
                    if (newBullets <= 0)
                        hasBullets.Value = false;
                    else
                        hasBullets.Value = true;
                };
                _timer.onEnd += () =>
                {
                    if (currentBullets.Value < maxBullets.Value)
                    {
                        currentBullets.Value++;
                        onReload?.Invoke();
                    }
                };
                shooter.onShootPerformed += () =>
                {
                    currentBullets.Value--;
                };
                currentBullets.Value = maxBullets.Value;
            }
        }

        [Serializable]
        public class HeroStates
        {
            public StateMachine<HeroStateType> stateMachine;

            [Section] 
            public IdleState idleState = new IdleState();

            [Section] 
            public RunState runState = new RunState();
            
            [Section] 
            public ShootState shootState = new ShootState();

            [Section] 
            public DeathState deathState = new DeathState();

            [Construct]
            public void Construct(HeroModel model)
            {
                model.onStart += () =>
                {
                    stateMachine.SwitchState(HeroStateType.Shoot);
                    stateMachine.Enter();
                };

                stateMachine.SetupStates(
                    (HeroStateType.Idle, idleState),
                    (HeroStateType.Run, runState),
                    (HeroStateType.Shoot, shootState),
                    (HeroStateType.Death, deathState)
                    );
            }

            [Construct]
            public void ConstructTransitions(LifeSection life, MoveSection move, Shoot shoot,
                ShootReload reload)
            {
                life.onDeath += () =>
                {
                    stateMachine.SwitchState(HeroStateType.Death);
                };
                move.onMoveStart += () =>
                {
                    if (stateMachine.CurrentState != HeroStateType.Death)
                    {
                        stateMachine.SwitchState(HeroStateType.Run);
                    }
                };
                move.onMoveFinish += () =>
                {
                    if (stateMachine.CurrentState != HeroStateType.Death && 
                        reload.currentBullets.Value > 0)
                    {
                        stateMachine.SwitchState(HeroStateType.Shoot);
                    }
                    if (stateMachine.CurrentState != HeroStateType.Death && 
                        reload.currentBullets.Value == 0)
                    {
                        stateMachine.SwitchState(HeroStateType.Idle);
                    }
                };
                shoot.onShootPerformed += () =>
                {
                    if (stateMachine.CurrentState != HeroStateType.Death && 
                        reload.currentBullets.Value == 0)
                    {
                        stateMachine.SwitchState(HeroStateType.Idle);
                    }
                };
                reload.onReload += () =>
                {
                    if (stateMachine.CurrentState != HeroStateType.Death && 
                        reload.currentBullets.Value > 0 &&
                        stateMachine.CurrentState != HeroStateType.Run)
                    {
                        stateMachine.SwitchState(HeroStateType.Shoot);
                    }
                };
            }
        }
    }
}