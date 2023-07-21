using Homeworks5.Components;
using System;
using UnityEngine;

namespace Homeworks5.Bullet
{
    [RequireComponent(typeof(BulletModel))]
    public class BulletEntity : Entity
    {
        private BulletModel _model;

        private void Awake()
        {
            _model = GetComponent<BulletModel>();

            this.Add(new DirectionComponent(_model.core.mover.Direction));
        }
    }
}
