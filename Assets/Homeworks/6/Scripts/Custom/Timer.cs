using Declarative;
using System;

namespace Homeworks6.Custom
{
    [Serializable]
    public class Timer : IDisposable
    {
        public event Action onEnd;
        private DeclarativeModel _model;
        private float _maxTime;
        private float _timer = 0f;
        private bool _canTime = true;

        public Timer(float maxTime, DeclarativeModel model, bool isReady = false)
        {
            this._maxTime = maxTime;
            this._model = model;
            if (isReady)
                _timer = _maxTime;
            Init();
        }

        private void Init()
        {
            _model.onUpdate += deltaTime =>
            {
                _timer += deltaTime;
                if (_timer >= _maxTime && _canTime)
                {
                    _canTime = false;
                    onEnd?.Invoke();
                }
            };
        }

        public void ResetTimer()
        {
            _timer = 0f;
            _canTime = true;
        }

        public void Dispose()
        {
        }
    }
    [Serializable]
    public class AutoTimer : IDisposable
    {
        public event Action onEnd;
        private Timer _timer;

        public AutoTimer(float maxTime, DeclarativeModel model, bool isReady = false)
        {
            _timer = new Timer(maxTime, model, isReady);
            Init();
        }

        private void Init()
        {
            _timer.onEnd += () =>
            {
                onEnd?.Invoke();
                _timer.ResetTimer();
            };
        }

        public void Dispose()
        {
            _timer.Dispose();
            _timer = null;
        }
    }
}