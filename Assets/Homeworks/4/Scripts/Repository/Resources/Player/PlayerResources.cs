using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Homeworks.SaveLoad
{
    public sealed class PlayerResources : MonoBehaviour
    {
        [ShowInInspector, ReadOnly]
        private Dictionary<ResourceType, int> _resources = new();

        public void AddResource(ResourceType resourceType, int quantity)
        {
            if (!_resources.ContainsKey(resourceType))
                _resources[resourceType] = quantity;
            _resources[resourceType] += quantity;
        }
        
        public void SetResource(ResourceType resourceType, int resource)
        {
            _resources[resourceType] = resource;
        }
        
        public int GetResource(ResourceType resourceType)
        {
            return _resources[resourceType];
        }

        public void SetAllResources(Dictionary<ResourceType, int> resources)
        {
            _resources = resources;
        }

        public Dictionary<ResourceType, int> GetAllResources()
        {
            return _resources;
        }
    }
}