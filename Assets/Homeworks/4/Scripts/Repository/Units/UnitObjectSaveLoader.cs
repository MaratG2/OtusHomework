using UnityEngine;

namespace Homeworks.SaveLoad
{
    public class UnitObjectSaveLoader : SaveLoader<UnitObjectData, UnitObject>
    {
        protected override void SetupData(UnitObject service, UnitObjectData data)
        {
            service.gameObject.transform.position = data.position;
            service.gameObject.transform.rotation = data.rotation;
            service.gameObject.transform.localScale = data.scale;
            service.hitPoints = data.hitPoints;
            service.speed = data.speed;
            service.damage = data.damage;
            Debug.Log($"<color=yellow>LOAD. UnitObject Service: {service.gameObject.name}. data: {data.key}!</color>");
        }

        protected override UnitObjectData ConvertToData(UnitObject service)
        {
            Debug.Log($"<color=green>SAVE. UnitObject: {service.gameObject.name}!</color>");
            return new UnitObjectData
            {
                key = service.gameObject.name,
                position = service.gameObject.transform.position,
                rotation = service.gameObject.transform.rotation,
                scale = service.gameObject.transform.localScale,
                hitPoints = service.hitPoints,
                speed = service.speed,
                damage = service.damage
            };
        }
    }
}