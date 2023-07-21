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

            this.Add(new MoveComponent(_model.core.move.onMove));
            this.Add(new ShootComponent(_model.core.shoot.onShoot));
            this.Add(new RotateComponent(_model.view.onRotate));
            this.Add(new TakeDamageComponent(_model.core.life.onTakeDamage));
            this.Add(new HeroStatsComponent
                (
                    _model.core.life.health,
                    _model.core.shootReload.currentBullets,
                    _model.core.shootReload.maxBullets,
                    _model.core.shoot.kills
                ));
            this.Add(new ScoresComponent(_model.core.shoot.onKilled));
        }
    }
}
