using Declarative;
using UnityEngine;

namespace Homeworks6.Bullet
{
    public class BulletModel : DeclarativeModel
    {
        [Section] 
        [SerializeField] 
        public BulletModel_Core core = new();
    }
}