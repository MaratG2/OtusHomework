using Homework7.Ecs.Components;
using Homework7.Ecs.Components.Block;
using Homeworks7;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Homework7.Ecs.Systems
{
    public struct SetSpawnPositionSystem : IEcsInitSystem
    {
        private EcsFilterInject<Inc<Position_C, Team_C>> _positionTeamFilter;
        private EcsCustomInject<SharedData> _data;
        public void Init(IEcsSystems systems)
        {
            var poolPositions = _positionTeamFilter.Pools.Inc1;
            var poolTeams = _positionTeamFilter.Pools.Inc2;

            foreach (int entity in _positionTeamFilter.Value)
            {
                ref var positionC = ref poolPositions.Get(entity);
                ref var teamC = ref poolTeams.Get(entity);

                float posX;
                float posY;
                int id = entity;
                if (teamC.team == Team.Blue)
                {
                    posX = _data.Value.leftPoint.x - _data.Value.gap * (id % 10);
                    posY = _data.Value.leftPoint.y - _data.Value.gap * (id / 10);
                }
                else
                {
                    id -= 100;
                    posX = _data.Value.rightPoint.x + _data.Value.gap * (id % 10);
                    posY = _data.Value.rightPoint.y - _data.Value.gap * (id / 10);
                }
                positionC.position = new Vector2(posX, posY);
            }
        }
    }
}