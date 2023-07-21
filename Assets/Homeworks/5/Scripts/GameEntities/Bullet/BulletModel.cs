using Atomic;
using Declarative;
using UnityEngine;

namespace Homeworks5.Bullet
{
    public class BulletModel : DeclarativeModel
    {
        [Section] 
        [SerializeField] 
        public BulletModel_Core core = new();

        [Section] 
        [SerializeField] 
        public BulletModel_View view = new();
    }
}