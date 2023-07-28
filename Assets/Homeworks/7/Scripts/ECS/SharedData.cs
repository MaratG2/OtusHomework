using System;
using Homework7.Ecs.Views;
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
        public EcsMonoObject prefabCube;
        
        public EcsMonoObject prefabBullet;

        public int damage;
        public Vector2 healthRange;
        public float movementSpeed;
        public float reloadTime;
        public Color colorBlue;
        public Color colorRed;
    }
}