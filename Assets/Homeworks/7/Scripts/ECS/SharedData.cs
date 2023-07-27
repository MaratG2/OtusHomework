using System;
using UnityEngine;

namespace Homework7.Ecs
{
    [Serializable]
    public class SharedData
    {
        public float borderX;
        public float borderY;
        public int countSpawn;
        public string prefabPath;

        public int damage;
        public int health;
        public float movementSpeed;
        public float reloadTime;
        public float detectionDistance;
        public Color colorBlue;
        public Color colorRed;
    }
}