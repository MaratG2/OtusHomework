using UnityEngine;

namespace Homework3.PM
{
    public class CharacterPresenter : MonoBehaviour, ICharacterPresenter
    {
        [SerializeField] private Sprite _expBarCompleted, _expBarUncompleted;
    }
}