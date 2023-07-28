
using Homework7.Ecs.Components.Cube;
using UnityEngine;

namespace Homework7.Ecs.Views
{
    public class BulletView : EcsMonoObject
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.transform.parent.TryGetComponent<EcsMonoObject>(out var target))
            {
                var poolTeamC = _world.GetPool<Team_C>();
                var bulletTeamC = poolTeamC.Get(this.GetEntity());
                var targetTeamC = poolTeamC.Get(target.GetEntity());
                if(bulletTeamC.team != targetTeamC.team)
                    OnBulletAction(this, target);
            }
        }
    }
}