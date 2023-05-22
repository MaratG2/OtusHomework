using Homeworks.h1.GameManagement;
using UnityEngine;

namespace Homeworks.h1
{
    public class ConsoleLogger : MonoBehaviour,
        IGameEndListener
    {
        public void OnGameEnd()
        {
            Debug.Log("Игра завершена");
        }
    }
}