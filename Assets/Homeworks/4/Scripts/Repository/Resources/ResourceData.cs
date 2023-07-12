using System;
using UnityEngine;

namespace Homeworks.SaveLoad
{
    [Serializable]
    public struct ResourceData
    {
        [SerializeField] public string key;
        [SerializeField] public ResourceType resourceType;
        [SerializeField] public int remainingCount;
    }
}