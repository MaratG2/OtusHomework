using System;
using UnityEngine;

namespace Homeworks.SaveLoad
{
    [Serializable]
    public struct UnitObjectData
    {
        [SerializeField] public string key;
        [SerializeField] public Vector3 position;
        [SerializeField] public Quaternion rotation;
        [SerializeField] public Vector3 scale;
        [SerializeField] public int hitPoints;
        [SerializeField] public int speed;
        [SerializeField] public int damage;
    }
}