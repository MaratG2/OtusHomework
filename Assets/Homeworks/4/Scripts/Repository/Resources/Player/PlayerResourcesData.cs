using System;
using System.Collections.Generic;
using UnityEngine;

namespace Homeworks.SaveLoad
{
    [Serializable]
    public struct PlayerResourcesData
    {
        [SerializeField] public Dictionary<ResourceType, int> _resources;
    }
}