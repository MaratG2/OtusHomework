using System.Collections.Generic;
using Homeworks6.Zombie;

namespace Homeworks6.Spawner
{
    public static class ZombieContainer
    {
        private static List<ZombieEntity> _zombies = new List<ZombieEntity>();

        public static List<ZombieEntity> Zombies => _zombies;

        public static void AddZombie(ZombieEntity zombie)
        {
            _zombies.Add(zombie);
        }
    }
}