using Homeworks6.Components;
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

            this.Add(new TakeDamageComponent(_model.core.life.onTakeDamage));
        }
    }
}
