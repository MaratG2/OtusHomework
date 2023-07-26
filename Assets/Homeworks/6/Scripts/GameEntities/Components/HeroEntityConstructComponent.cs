using Atomic;
using Homeworks6.Hero;
using UnityEngine;

namespace Homeworks6.Components
{
    public interface IHeroEntityConstructComponent
    {
        void Construct(HeroEntity entity);
    }

    public class HeroEntityConstructComponent : IHeroEntityConstructComponent
    {
        public AtomicEvent<HeroEntity> OnConstructEntity;

        public HeroEntityConstructComponent(AtomicEvent<HeroEntity> construct)
        {
            OnConstructEntity = construct;
        }
        void IHeroEntityConstructComponent.Construct(HeroEntity entity)
        {
            OnConstructEntity?.Invoke(entity);
        }
    }
}