using System;
using Atomic;
using Declarative;
using Homeworks6.Hero;

namespace Homeworks6
{
    [Serializable]
    public class PlayerMoveSection
    {
        [Section] 
        public MoveSection moveSection = new(false);
        
        private AtomicVariable<bool> _moveRequired = new();
        
        [Construct]
        public void Init(HeroModel model)
        {
            moveSection.Init(model);
            moveSection.moveEngine.onMove += _ =>
            {
                _moveRequired.Value = true;
            };
            model.onFixedUpdate += deltaTime =>
            {
                if (_moveRequired.Value)
                {
                    moveSection.moveEngine.onMoveEvent.Invoke(deltaTime);
                    _moveRequired.Value = false;
                }
            };
        }
    }
}