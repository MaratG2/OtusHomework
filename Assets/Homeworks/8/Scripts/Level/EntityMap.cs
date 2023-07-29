using System.Collections.Generic;
using UnityEngine;

namespace Homework8.Level
{
    public sealed class EntityMap
    {
        private readonly Dictionary<Vector2Int, IEntity> _entities = new Dictionary<Vector2Int, IEntity>();

        public bool HasEntity(Vector2Int coordinates)
        {
            return _entities.ContainsKey(coordinates);
        }

        public IEntity GetEntity(Vector2Int coordinates)
        {
            return _entities.ContainsKey(coordinates) ? _entities[coordinates] : null;
        }

        public void RemoveEntity(Vector2Int coordinates)
        {
            _entities.Remove(coordinates);
        }
        
        public void SetEntity(Vector2Int coordinates, IEntity entity)
        {
            _entities[coordinates] = entity;
        }
    }
}