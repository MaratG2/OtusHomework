using Atomic;
using System;
using UnityEngine;

namespace Homeworks5.Components
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
