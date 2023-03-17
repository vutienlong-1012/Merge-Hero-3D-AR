using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VTLTools;

namespace MergeAR.UI
{
    public class VibrationToggle : MonoBehaviour
    {
        [SerializeField] Toggle vibrationToggle;
        private void OnEnable()
        {
            vibrationToggle.isOn = StaticVariables.IsVibrationOn;
            vibrationToggle.onValueChanged.AddListener(delegate
            {
                VibrationSystem.Instance.ToggeVibration();
                SoundSystem.Instance.PlayUIClick();
                VibrationSystem.Instance.PlayVibration();
            });
        }
        private void OnDisable()
        {
            vibrationToggle.onValueChanged.RemoveListener(delegate
            {
                VibrationSystem.Instance.ToggeVibration();
                SoundSystem.Instance.PlayUIClick();
                VibrationSystem.Instance.PlayVibration();
            });
        }
    }
}