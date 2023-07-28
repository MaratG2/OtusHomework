using Homework7.Ecs;
using UnityEngine;

namespace Homework7.Helpers
{
    public static class CheckOutOfBounds
    {
        public static bool IsOut(Vector3 origin, SharedData data)
        {
            bool isOut = false;
            if (origin.x > data.borderX || origin.x < -data.borderX)
                isOut = true;
            if (origin.z > data.borderY || origin.z < -data.borderY)
                isOut = true;
            return isOut;
        }
    }
}