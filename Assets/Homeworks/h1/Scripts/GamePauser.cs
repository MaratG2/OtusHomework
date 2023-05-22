using Homeworks.h1.GameManagement;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Homeworks.h1
{
    public class GamePauser : MonoBehaviour
    {
        [SerializeField, Required] private GameManager _gameManager;

        public void Pause()
        {
            _gameManager.PauseGame();
        }

        public void Resume()
        {
            _gameManager.ResumeGame();
        }
    }
}