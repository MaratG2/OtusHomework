using System;
using Atomic;
using Declarative;
using Homeworks6.Custom;
using Homeworks6.Hero.Custom;
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
        public AtomicVariable<Entity> target = new();
        
        [Section]
        public PlayerMoveSection playerMoveSection = new();
        
        [Section]
        public HeroStatesSection heroStatesSection = new();
        
        [Serializable]
        public class ShootSection
        {
            [Section] 
            public AutoShootObserver autoShootObserver = new();
            
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
                    if (shootReloader.ammo.hasBullets.Value && !_shootTimer.IsPlaying && !life.isDead.Value)
                    {
                        _shootTimer.Start();
                        _bulletFactory.Create();
                        onShootEvent?.Invoke();
                    }
                });
                onShootEvent += () =>
                {
                    shootReloader.ammo.RemoveAmmo();
                };
            }
        }

        [Serializable]
        public class ShootReloadSection
        {
            [Section] 
            public BulletAmmo ammo;
            [SerializeReference] private Timer _timer;

            [Construct]
            public void Init(HeroModel model)
            {
                _timer.Start();
                _timer.onEnd += () =>
                {
                   ammo.AddAmmo();
                };
            }
        }

        [Serializable]
        public class BulletAmmo
        {
            [SerializeField] public AtomicVariable<int> maxBullets;
            [HideInInspector] public AtomicVariable<int> currentBullets;
            [HideInInspector] public AtomicVariable<bool> hasBullets;

            [Construct]
            public void Init()
            {
                currentBullets.OnChanged += newBullets =>
                {
                    if (newBullets <= 0)
                        hasBullets.Value = false;
                    else
                        hasBullets.Value = true;
                };
                
                currentBullets.Value = maxBullets.Value;
            }

            public void AddAmmo()
            {
                if (currentBullets.Value < maxBullets.Value)
                {
                    currentBullets.Value++;
                }
            }

            public void RemoveAmmo()
            {
                if (currentBullets.Value > 0)
                {
                    currentBullets.Value--;
                }
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
            public DeathState deathState = new();

            [Construct]
            public void Construct(HeroModel model)
            {
                model.onStart += () =>
                {
                    stateMachine.SwitchState(HeroStateType.Idle);
                    stateMachine.Enter();
                };

                stateMachine.SetupStates(
                    (HeroStateType.Idle, idleState),
                    (HeroStateType.Run, runState),
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
                    if (stateMachine.CurrentState != HeroStateType.Death)
                    {
                        stateMachine.SwitchState(HeroStateType.Idle);
                    }
                };
            }
        }
    }
}