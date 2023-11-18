using Homework7.Ecs.Components;
using Homework7.Ecs.Components.Cube;
using Homework7.Enums;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Homework7.Ecs.Systems
{
    public struct SetSpawnPositionSystem : IEcsInitSystem
    {
        private EcsFilterInject<Inc<Position_C, Team_C>> _positionTeamFilter;
        private EcsCustomInject<WorldSO> _worldData;
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
                    posX = _worldData.Value.LeftPoint.x - _worldData.Value.Gap * (id % 10);
                    posY = _worldData.Value.LeftPoint.y - _worldData.Value.Gap * (id / 10);
                }
                else
                {
                    id -= 100;
                    posX = _worldData.Value.RightPoint.x + _worldData.Value.Gap * (id % 10);
                    posY = _worldData.Value.RightPoint.y - _worldData.Value.Gap * (id / 10);
                }
                positionC.position = new Vector2(posX, posY);
            }
        }
    }
}