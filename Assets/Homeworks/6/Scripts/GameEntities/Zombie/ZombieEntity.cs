﻿using Homeworks6.Components;
using UnityEngine;

namespace Homeworks6.Zombie
{
    [RequireComponent(typeof(ZombieModel))]
    public class ZombieEntity : Entity
    {
        private ZombieModel _model;

        private void Awake()
        {
            _model = GetComponent<ZombieModel>();

            this.Add(new TakeDamageComponent(_model.core.lifeSection.onTakeDamage));
            this.Add(new HeroEntityConstructComponent(_model.onConstruct));
            this.Add(new GetHPComponent(_model.core.lifeSection));
            this.Add(new DeathEventComponent(_model.core.lifeSection.onDeath));
        }
    }
}