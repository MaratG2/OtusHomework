using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Homeworks6.Custom
{
    [Serializable]
    public class DefaultFX : IFX
    {
        [SerializeField, Required] private AudioSource _audioSource;
        [SerializeField] private ParticleSystem _VFX;
        [SerializeField] private AudioClip _SFX;

        public void Play()
        {
            _VFX.Play();
            _audioSource.PlayOneShot(_SFX);
        }
    }
}