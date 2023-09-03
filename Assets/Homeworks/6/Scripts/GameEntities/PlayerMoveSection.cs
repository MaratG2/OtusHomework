using System;
using Atomic;
using Declarative;

namespace Homeworks6
{
    [Serializable]
    public class PlayerMoveSection
    {
        [Section] 
        public MoveSection moveSection = new(false);
        
        private AtomicVariable<bool> _moveRequired = new();
        
        public void Init(DeclarativeModel model)
        {
            moveSection.Init(model);
            moveSection.onMove += _ =>
            {
                _moveRequired.Value = true;
            };
            model.onFixedUpdate += deltaTime =>
            {
                if (_moveRequired.Value)
                {
                    moveSection.onMoveEvent.Invoke(deltaTime);
                    _moveRequired.Value = false;
                }
            };
        }
    }
}