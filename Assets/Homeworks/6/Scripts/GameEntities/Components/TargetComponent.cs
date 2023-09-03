using Atomic;

namespace Homeworks6.Components
{
    public interface ITargetComponent
    {
        AtomicVariable<Entity> GetTarget();
    }

    public class TargetComponent : ITargetComponent
    {
        private AtomicVariable<Entity> _target;
        public TargetComponent(AtomicVariable<Entity> target)
        {
            this._target = target;
        }

        public AtomicVariable<Entity> GetTarget()
        {
            return _target;
        }
    }
}