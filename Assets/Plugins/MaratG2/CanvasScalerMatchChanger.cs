using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MaratG2
{
    [RequireComponent(typeof(CanvasScaler))]
    public class CanvasScalerMatchChanger : MonoBehaviour
    {
        private CanvasScaler _canvasScaler;
        private readonly float _defaultRatio = 16 / 9f - Mathf.Epsilon;
        private float _deviceRatio = 16 / 9f;

        private void Awake()
        {
            _canvasScaler = GetComponent<CanvasScaler>();
        }

        private void Update()
        {
            _deviceRatio = (float)Screen.width / Screen.height;
            if (_deviceRatio >= _defaultRatio)
                _canvasScaler.matchWidthOrHeight = 1f;
            else
                _canvasScaler.matchWidthOrHeight = 0f;
        }
    }
}