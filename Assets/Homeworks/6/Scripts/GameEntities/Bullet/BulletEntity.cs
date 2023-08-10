using Homeworks6.Components;
using UnityEngine;

namespace Homeworks6.Bullet
{
    [RequireComponent(typeof(BulletModel))]
    public class BulletEntity : Entity
    {
        private BulletModel _model;

        private void Awake()
        {
            _model = GetComponent<BulletModel>();

            this.Add(new DirectionComponent(_model.core.moveSection.Direction));
        }
    }
}
