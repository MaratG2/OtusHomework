using UnityEngine;

namespace Homeworks.SaveLoad
{
    public class PlayerResourceSaveLoader : SaveLoader<PlayerResourcesData, PlayerResources>
    {
        protected override void SetupData(PlayerResources resources, PlayerResourcesData data)
        {
            resources.SetAllResources(data._resources);
            Debug.Log($"<color=yellow>LOAD. Resources: {resources.GetAllResources().Count}!</color>");
        }

        protected override PlayerResourcesData ConvertToData(PlayerResources resources)
        {
            Debug.Log($"<color=green>SAVE. Resources: {resources.GetAllResources().Count}!</color>");
            return new PlayerResourcesData
            {
                _resources = resources.GetAllResources()
            };
        }
    }
}