using Atomic;

namespace Homeworks6.Components
{
    public interface IScoresComponent
    {
        void AddScore();
    }

    public class ScoresComponent : IScoresComponent
    {
        public IAtomicAction onScoreAdd;

        public ScoresComponent(IAtomicAction onScoreAdd)
        {
            this.onScoreAdd = onScoreAdd;
        }

        void IScoresComponent.AddScore()
        {
            onScoreAdd?.Invoke();
        }
    }
}
