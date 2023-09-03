using Homeworks6.Components;
using Homeworks6.Hero;
using UnityEngine;
using Zenject;

namespace Homeworks6.Controllers
{
    public class DeathController : MonoBehaviour
    {
        private HeroEntity _heroEntity;

        [Inject]
        private void Construct(HeroEntity heroEntity)
        {
            this._heroEntity = heroEntity;
        }

        private void Start()
        {
            _heroEntity.Get<DeathEventComponent>().OnDeath += Death;
        }

        private void Death()
        {
            Debug.Log("ИГРА ЗАВЕРШЕНА");
        }

        private void OnDestroy()
        {
            _heroEntity.Get<DeathEventComponent>().OnDeath -= Death;
        }
    }
}