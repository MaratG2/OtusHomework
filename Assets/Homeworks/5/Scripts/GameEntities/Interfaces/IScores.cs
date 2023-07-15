using Atomic;

namespace Homeworks5.Interfaces
{
    public interface IScores
    {
        public AtomicVariable<int> Kills { get; }
    }
}