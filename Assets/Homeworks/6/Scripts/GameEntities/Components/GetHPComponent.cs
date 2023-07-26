using Atomic;

namespace Homeworks6.Components
{
    public interface IGetHPComponent
    {
        int GetHP();
    }

    public class GetHPComponent : IGetHPComponent
    {
        private LifeSection _life;
        public GetHPComponent(LifeSection life)
        {
            _life = life;
        }

        int IGetHPComponent.GetHP()
        {
            return _life.health.Value;
        }
    }
}