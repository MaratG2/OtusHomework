using Homeworks.h1.GameManagement;
using UnityEngine;

namespace Homeworks.h1
{
    public class ConsoleController : MonoBehaviour,
        IGameEndListener
    {
        public void OnGameEnd()
        {
            Debug.Log("Игра завершена");
        }
    }
}