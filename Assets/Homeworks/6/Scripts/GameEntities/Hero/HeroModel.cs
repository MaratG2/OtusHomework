using Declarative;
using UnityEngine;

namespace Homeworks6.Hero
{
    public class HeroModel : DeclarativeModel
    {
        [Section] 
        [SerializeField] 
        public HeroModel_Core core = new HeroModel_Core();

        [Section] 
        [SerializeField] 
        public HeroModel_View view = new HeroModel_View();
    }
}