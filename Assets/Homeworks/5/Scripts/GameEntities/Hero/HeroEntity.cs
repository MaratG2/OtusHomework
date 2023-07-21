using System;
using UnityEngine;
using Atomic;
using Declarative;
using Homeworks5.Components;
using Zenject;

namespace Homeworks5.Hero
{
    [RequireComponent(typeof(HeroModel))]
    public class HeroEntity : Entity
    {
        private HeroModel _model;

        private void Awake()
        {
            _model = GetComponent<HeroModel>();

            this.Add(new MoveComponent(_model.core.mover.onMove));
            this.Add(new ShootComponent(_model.core.shooter.onShoot));
            this.Add(new RotateComponent(_model.view.onRotate));
            this.Add(new TakeDamageComponent(_model.core.life.onTakeDamage));
            this.Add(new HeroStatsComponent
                (
                    _model.core.life.health,
                    _model.core.shootReloader.currentBullets,
                    _model.core.shootReloader.maxBullets,
                    _model.core.shooter.kills
                ));
            this.Add(new ScoresComponent(_model.core.shooter.onKilled));
        }
    }
}
