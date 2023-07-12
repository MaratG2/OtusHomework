using UnityEngine;

namespace Homeworks.SaveLoad
{
    public class ResourceSaveLoader : SaveLoader<ResourceData, ResourceObject>
    {
        protected override void SetupData(ResourceObject service, ResourceData data)
        {
            service.resourceType = data.resourceType;
            service.remainingCount = data.remainingCount;
            Debug.Log($"<color=yellow>LOAD. Resource Service: {service.gameObject.name}. data: {data.key}!</color>");
        }

        protected override ResourceData ConvertToData(ResourceObject service)
        {
            Debug.Log($"<color=green>SAVE. Resource: {service.gameObject.name}!</color>");
            return new ResourceData
            {
                key = service.gameObject.name,
                resourceType = service.resourceType,
                remainingCount = service.remainingCount
            };
        }
    }
}