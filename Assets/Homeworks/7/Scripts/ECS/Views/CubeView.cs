using Homework7.Ecs.Components.Cube;
using UnityEngine;

namespace Homework7.Ecs.Views
{
    public class CubeView : EcsMonoObject
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent<EcsMonoObject>(out var secondCollide))
            {
                var poolTeamC = _world.GetPool<Team_C>();
                var firstTeamC = poolTeamC.Get(this.GetEntity());
                var secondTeamC = poolTeamC.Get(secondCollide.GetEntity());
                
                if (firstTeamC.team != secondTeamC.team)
                {
                    OnFightAction(this, secondCollide);
                }
            }
        }
    }
}