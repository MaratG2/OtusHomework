using System;
using UnityEngine;

namespace Homework7.Ecs
{
    [Serializable]
    public class SharedData
    {
        public Transform parent;
        
        public float borderX;
        public float borderY;
        public float gap;
        public Vector2 leftPoint;
        public Vector2 rightPoint;
        
        public int countSpawn;
        public GameObject prefab;

        public int damage;
        public Vector2 healthRange;
        public float movementSpeed;
        public float reloadTime;
        public float detectionDistance;
        public Color colorBlue;
        public Color colorRed;
    }
}