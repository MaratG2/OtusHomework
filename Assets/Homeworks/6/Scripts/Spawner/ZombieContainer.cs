using System;
using System.Collections.Generic;
using Homeworks6.Components;
using Homeworks6.Zombie;
using UnityEngine;

namespace Homeworks6.Spawner
{
    public static class ZombieContainer
    {
        private static List<ZombieEntity> _zombies = new List<ZombieEntity>();
        public static List<ZombieEntity> Zombies => _zombies;

        public static event Action OnZombieDeath; 

        public static void AddZombie(ZombieEntity zombie)
        {
            zombie.Get<DeathEventComponent>().OnDeath += () => OnZombieDeath?.Invoke();
            _zombies.Add(zombie);
        }
    }
}