using Lessons.Architecture.PM;
using UnityEngine;

namespace Homework3.PM
{
    public class CharacterStatModel : MonoBehaviour
    {
        private CharacterStat _stat;
        public CharacterStat Stat => _stat;

        public void Init(CharacterStat stat)
        {
            this._stat = stat;
        }
    }
}