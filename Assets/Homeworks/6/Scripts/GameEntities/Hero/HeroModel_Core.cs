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
        public LifeSection lifeSection = new();

        [Section]
        public ShootSection shootSection = new();

        [Section]
        public ShootReloadSection shootReloadSection = new();

        [Section]
        public PlayerMoveSection playerMoveSection = new();
        
        [Section]
        public HeroStatesSection heroStatesSection = new();

        [Serializable]
        public class ShootSection
        {
            [SerializeField] private BulletFactory _bulletFactory = new();
            [SerializeReference] private Timer _shootTimer;
            [HideInInspector] public AtomicEvent onRequestShoot;
            [HideInInspector] public AtomicEvent onShootEvent;
            
            [Construct]
            public void Init(LifeSection life, ShootReloadSection shootReloader, HeroModel model)
            {
                _shootTimer.Start();
                onRequestShoot.AddListener(() =>
                {
                    if (shootReloader.hasBullets.Value && !_shootTimer.IsPlaying && !life.isDead.Value)
                    {
                        _shootTimer.Start();
                        _bulletFactory.Create();
                        onShootEvent?.Invoke();
                    }
                });
            }
        }

        [Serializable]
        public class ShootReloadSection
        {
            [SerializeField] public AtomicVariable<int> maxBullets;
            [HideInInspector] public AtomicVariable<int> currentBullets;
            [HideInInspector] public AtomicVariable<bool> hasBullets;
            [HideInInspector] public AtomicEvent onReload;
            [SerializeReference] private Timer _timer;

            [Construct]
            public void Init(ShootSection shooter, HeroModel model)
            {
                _timer.Start();
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
                shooter.onShootEvent += () =>
                {
                    currentBullets.Value--;
                };
                currentBullets.Value = maxBullets.Value;
            }
        }
        
        [Serializable]
        public class HeroStatesSection
        {
            public StateMachine<HeroStateType> stateMachine;

            [Section] 
            public IdleState idleState = new();

            [Section] 
            public RunState runState = new();
            
            [Section] 
            public ShootState shootState = new();

            [Section] 
            public DeathState deathState = new();

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
            public void ConstructTransitions(LifeSection life, MoveSection move, ShootSection shootSection,
                ShootReloadSection shootReloadSection)
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
                        shootReloadSection.currentBullets.Value > 0)
                    {
                        stateMachine.SwitchState(HeroStateType.Shoot);
                    }
                    if (stateMachine.CurrentState != HeroStateType.Death && 
                        shootReloadSection.currentBullets.Value == 0)
                    {
                        stateMachine.SwitchState(HeroStateType.Idle);
                    }
                };
                shootSection.onShootEvent += () =>
                {
                    if (stateMachine.CurrentState != HeroStateType.Death && 
                        shootReloadSection.currentBullets.Value == 0)
                    {
                        stateMachine.SwitchState(HeroStateType.Idle);
                    }
                };
                shootReloadSection.onReload += () =>
                {
                    if (stateMachine.CurrentState != HeroStateType.Death && 
                        shootReloadSection.currentBullets.Value > 0 &&
                        stateMachine.CurrentState != HeroStateType.Run)
                    {
                        stateMachine.SwitchState(HeroStateType.Shoot);
                    }
                };
            }
        }
    }
}