using Homework7.Ecs;
using UnityEngine;

namespace Homework7.Helpers
{
    public static class CheckOutOfBounds
    {
        public static bool IsOut(Vector3 origin, WorldSO worldData)
        {
            bool isOut = false;
            if (origin.x > worldData.BorderX || origin.x < -worldData.BorderX)
                isOut = true;
            if (origin.z > worldData.BorderY || origin.z < -worldData.BorderY)
                isOut = true;
            return isOut;
        }
    }
}