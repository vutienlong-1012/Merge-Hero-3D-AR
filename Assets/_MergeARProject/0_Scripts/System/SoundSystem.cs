using System.Collections;
using System.Collections.Generic;
using VTLTools;
using UnityEngine;


namespace MergeAR
{
    public class SoundSystem : Singleton<SoundSystem>
    {
        [SerializeField] AudioSource shareAudioSource;
        [SerializeField] AudioSource uIAudioSource;
        [SerializeField] AudioClip uIOnClickAudioClip;
        public void PlaySoundOneShot(AudioSource _audioSource, AudioClip _audioClip)
        {
            if (!StaticVariables.IsSoundOn)
                return;
            else
                _audioSource.PlayOneShot(_audioClip);
        }

        public void PlaySoundOneShot(AudioSource _audioSource, AudioClip _audioClip, float _volume)
        {
            if (!StaticVariables.IsSoundOn)
                return;
            else
                _audioSource.PlayOneShot(_audioClip, _volume);
        }

        public void PlayUIClick()
        {
            if (!StaticVariables.IsSoundOn)
                return;
            else
                uIAudioSource.PlayOneShot(uIOnClickAudioClip);
        }

        public void ToggeSound()
        {
            StaticVariables.IsSoundOn = !StaticVariables.IsSoundOn;
        }
    }
}