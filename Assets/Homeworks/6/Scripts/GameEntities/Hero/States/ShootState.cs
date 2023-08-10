using System;
using Atomic;
using Declarative;

namespace Homeworks6.Hero.States
{
    [Serializable]
    public class ShootState : CompositeState
    {
        [Construct]
        public void Construct(HeroModel_Core.ShootSection shootSection, HeroModel model)
        {
            var autoShootState = new AutoShootState(shootSection, model);
            var aimAiState = new AimAIState(model);
            SetStates(autoShootState, aimAiState);
        }
    }
}