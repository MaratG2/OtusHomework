using System.Linq;
using UnityEngine;
using Zenject;

namespace ShootEmUp.Level
{
    public sealed class LevelBoundsController : MonoBehaviour
    {
        private Transform _leftBorder, _rightBorder, _downBorder, _topBorder;
        
        [Inject]
        private void Construct(Transform[] borders)
        {
            borders = borders.Skip(1).ToArray();
            this._leftBorder = borders[0];
            this._rightBorder = borders[1];
            this._downBorder = borders[2];
            this._topBorder = borders[3];
        }
        
        public bool IsInBounds(Vector3 position)
        {
            return position.x > _leftBorder.position.x
                   && position.x < _rightBorder.position.x
                   && position.y > _downBorder.position.y
                   && position.y < _topBorder.position.y;
        }
    }
}