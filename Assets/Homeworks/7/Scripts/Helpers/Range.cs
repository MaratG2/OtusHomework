using System;
using UnityEngine;

namespace Homework7.Helpers
{
    [Serializable]
    public class IntRange
    {
        [SerializeField] private Vector2 _range;

        private int _value;
        public int Value
        {
            get
            {
                if (!_isGenerated)
                {
                    _value = GenerateValue();
                    _isGenerated = true;
                }
                return _value;
            }
            private set => _value = value;
        }
        private bool _isGenerated;

        private int GenerateValue()
        {
            return UnityEngine.Random.Range((int)_range.x, (int)_range.y);
        }
    }
}