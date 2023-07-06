using UnityEngine;

namespace ShootEmUp.GameManagement
{
    public class GameEndBehaviour : MonoBehaviour,
        IGameEndListener
    {
        public void OnGameEnd()
        {
            Debug.Log("Вы проиграли");
            Time.timeScale = 0f;
        }
    }
}