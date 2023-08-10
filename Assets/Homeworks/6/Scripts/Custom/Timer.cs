using System;
using System.Threading.Tasks;
using UnityEngine;

#pragma warning disable 4014

namespace Homeworks6.Custom
{
    [Serializable]
    public class Timer
    {
        [SerializeField] private float _maxTime;
        [SerializeField] private bool _isAuto;
        public event Action onEnd;
        public bool IsPlaying { get; private set; } = true;

        public void Start()
        {
            IsPlaying = true;
            WaitTimer();
        }
        
        private async Task WaitTimer()
        {
            await Task.Delay(Mathf.FloorToInt(_maxTime * 1000));
            IsPlaying = false;
            onEnd?.Invoke();
            if(_isAuto)
                Start();
        }
    }
}