using UnityEngine;

namespace Homework7.Ecs.Views
{
    public class CubeView : EcsMonoObject
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent<EcsMonoObject>(out var secondCollide))
                OnTriggerAction(this, secondCollide);
        }
    }
}