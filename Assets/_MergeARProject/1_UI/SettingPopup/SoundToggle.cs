using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VTLTools;
using Sirenix.OdinInspector;

namespace MergeAR.UI
{
    public class SoundToggle : MonoBehaviour
    {
        [SerializeField] Toggle soundToggle;
        private void OnEnable()
        {
            soundToggle.isOn = StaticVariables.IsSoundOn;
            soundToggle.onValueChanged.AddListener(delegate
            {
                SoundSystem.Instance.ToggeSound();
                SoundSystem.Instance.PlayUIClick();
                VibrationSystem.Instance.PlayVibration();
            });
        }
        private void OnDisable()
        {
            soundToggle.onValueChanged.RemoveListener(delegate
            {
                SoundSystem.Instance.ToggeSound();
                SoundSystem.Instance.PlayUIClick();
                VibrationSystem.Instance.PlayVibration();
            });
        }    
    }
}