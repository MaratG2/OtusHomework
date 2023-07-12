using System.Collections.Generic;
using Homeworks.SaveLoad;
using UnityEngine;
using Zenject;

public class PlayerBehaviour : MonoBehaviour
{
    private PlayerResources _resources;
    private readonly List<ResourceHarvester> _resourceHarvesters = new();
    
    [Inject]
    private void Construct(PlayerResources resources)
    {
        this._resources = resources;
    }

    private void Awake()
    {
        _resourceHarvesters.Add(new ResourceHarvester
        {
            resource = ResourceType.FOOD,
            timeToHarvest = 2f
        });
        _resourceHarvesters.Add(new ResourceHarvester
        {
            resource = ResourceType.WOOD,
            timeToHarvest = 1f
        });
        _resourceHarvesters.Add(new ResourceHarvester
        {
            resource = ResourceType.MONEY,
            timeToHarvest = 2.5f
        });
        _resourceHarvesters.Add(new ResourceHarvester
        {
            resource = ResourceType.STONE,
            timeToHarvest = 3f
        });
    }

    private void Update()
    {
        foreach (var harvester in _resourceHarvesters)
        {
            harvester.timer += Time.deltaTime;
            if (harvester.timer > harvester.timeToHarvest)
            {
                _resources.AddResource(harvester.resource, 1);
                harvester.timer -= harvester.timeToHarvest;
            }
        }
    }

    private class ResourceHarvester
    {
        public float timer = 0f;
        public float timeToHarvest;
        public ResourceType resource;
    }
}
