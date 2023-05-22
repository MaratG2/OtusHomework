using Cysharp.Threading.Tasks;
using Homeworks.h1.GameManagement;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Homeworks.h1
{
    public class GameStartTimer : MonoBehaviour
    {
        public UnityEvent OnGameStart;
        [SerializeField] private float _time = 3f;
        [SerializeField, Required] private GameManager _gameManager; 
        [SerializeField, Required] private TextMeshProUGUI _timerText;

        public void StartTimer()
        {
#pragma warning disable CS4014
            DelayStart();
#pragma warning restore CS4014
        }

        public async UniTask DelayStart()
        {
            float timer = 0f;
            while (timer < _time)
            {
                await UniTask.DelayFrame(1);
                timer += Time.deltaTime;
                _timerText.text = Mathf.CeilToInt(_time - timer).ToString();
            }
            OnGameStart?.Invoke();
            _gameManager.StartGame();
        }
    }
}