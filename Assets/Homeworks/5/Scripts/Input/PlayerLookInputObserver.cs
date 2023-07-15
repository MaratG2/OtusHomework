using Homeworks5.Hero;
using UnityEngine;

namespace Homeworks5.Input
{
    public class PlayerLookInputObserver : MonoBehaviour
    {
        //TODO: DI
        [SerializeField] private PlayerLookInput _playerLookInput;
        [SerializeField] private HeroModel _heroModel;

        private void OnEnable()
        {
            _playerLookInput.onLook += Look;
        }
        
        private void OnDisable()
        {
            _playerLookInput.onLook -= Look;
        }

        private void Look(Vector2 pos)
        {
            pos = ConvertToCenterPos(pos);
            Vector3 forward = new(pos.x, 0f, pos.y);
            _heroModel.view.onRotate?.Invoke(forward.normalized);
        }

        private Vector2 ConvertToCenterPos(Vector2 leftBottomPos)
        {
            return leftBottomPos - GetCenterPos();
        }

        private Vector2 GetCenterPos()
        {
            return new Vector2(Screen.width, Screen.height) / 2f;
        }
    }
}