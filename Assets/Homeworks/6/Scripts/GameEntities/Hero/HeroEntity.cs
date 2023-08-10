using UnityEngine;
using Homeworks6.Components;

namespace Homeworks6.Hero
{
    [RequireComponent(typeof(HeroModel))]
    public class HeroEntity : Entity
    {
        private HeroModel _model;

        private void Awake()
        {
            _model = GetComponent<HeroModel>();

            this.Add(new MoveComponent(_model.core.moveSection.onMove));
            this.Add(new ShootComponent(_model.core.shootSection.onRequestShoot));
            this.Add(new RotateComponent(_model.view.onRotate));
            this.Add(new TakeDamageComponent(_model.core.lifeSection.onTakeDamage));
            this.Add(new HeroStatsComponent
                (
                    _model.core.lifeSection.health,
                    _model.core.shootReloadSection.currentBullets,
                    _model.core.shootReloadSection.maxBullets
                ));
        }
    }
}
