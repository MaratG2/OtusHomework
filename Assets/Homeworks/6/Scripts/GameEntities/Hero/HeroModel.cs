using Atomic;
using Declarative;
using UnityEngine;

namespace Homeworks5.Hero
{
    public class HeroModel : DeclarativeModel
    {
        [Section] 
        [SerializeField] 
        public HeroModel_Core core = new();

        [Section] 
        [SerializeField] 
        public HeroModel_View view = new();
    }
}