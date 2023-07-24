using Homeworks5.Components;
using Homeworks5.Hero;
using UnityEngine;
using Zenject;

namespace Homeworks5.Input
{
    public class PlayerLookInputObserver : MonoBehaviour
    {
        private PlayerLookInput _playerLookInput;
        private HeroEntity _heroEntity;

        [Inject]
        private void Construct(PlayerLookInput input, HeroEntity heroEntity)
        {
            this._playerLookInput = input;
            this._heroEntity = heroEntity;
        }
        
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
            Vector3 forward = new Vector3(pos.x, 0f, pos.y);
            _heroEntity.Get<IRotateComponent>().Rotate(forward.normalized);
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