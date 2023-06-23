using VTLTools;
using UnityEngine;
using DG.Tweening;

namespace MergeAR
{
    public class MusicSystem : Singleton<MusicSystem>
    {
        [SerializeField] private AudioSource musicAudioSource;



        void OnEnable()
        {
            musicAudioSource.mute = !StaticVariables.IsMusicOn;
        }

        public void PlayMusic(AudioClip _audioClip)
        {
            if (!StaticVariables.IsMusicOn)
            {
                musicAudioSource.clip = _audioClip;
                return;
            }
            else
            {
                musicAudioSource.clip = _audioClip;
                musicAudioSource.Play();
            }
        }

        public void FadeMusicVolume(float _value, float _time)
        {
            musicAudioSource.DOFade(_value, _time);
        }

        public void ToggleMusic()
        {
            StaticVariables.IsMusicOn = !StaticVariables.IsMusicOn;
            musicAudioSource.mute = !StaticVariables.IsMusicOn;
        }
    }
}