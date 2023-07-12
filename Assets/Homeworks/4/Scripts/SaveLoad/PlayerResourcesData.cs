using System;
using System.Collections.Generic;
using UnityEngine;

namespace Homeworks.SaveLoad
{
    [Serializable]
    public class PlayerResourcesData
    {
        [SerializeField]
        public Dictionary<ResourceType, int> _resources = new();
    }
}