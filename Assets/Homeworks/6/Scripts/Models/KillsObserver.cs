using System;
using Homeworks6.Spawner;
using Zenject;

namespace Homeworks6.Models
{
    public class KillsObserver
    {
        public event Action OnKillsChanged;
        private KillsModel _killsModel;

        [Inject]
        private void Construct(KillsModel killsModel)
        {
            _killsModel = killsModel;
            
            ZombieContainer.OnZombieDeath += _killsModel.AddKill;
            ZombieContainer.OnZombieDeath += () => OnKillsChanged?.Invoke();
        }

        public int GetKills()
        {
            return _killsModel.Kills;
        }
    }
}