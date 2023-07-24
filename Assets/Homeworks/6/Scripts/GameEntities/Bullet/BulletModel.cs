using Declarative;
using UnityEngine;

namespace Homeworks6.Bullet
{
    public class BulletModel : DeclarativeModel
    {
        [Section] 
        [SerializeField] 
        public BulletModel_Core core = new BulletModel_Core();

        [Section] 
        [SerializeField] 
        public BulletModel_View view = new BulletModel_View();
    }
}