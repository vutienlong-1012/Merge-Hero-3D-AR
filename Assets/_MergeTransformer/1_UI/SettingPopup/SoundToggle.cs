using UnityEngine;
using UnityEngine.UI;
using VTLTools;

namespace MergeAR.UI
{
    public class SoundToggle : SettingToggle
    {
        protected override void OnEnable()
        {
            toggle.isOn = StaticVariables.IsSoundOn;
            base.OnEnable();
        }
        protected override void OnDisable()
        {
            base.OnDisable();
        }

        protected override void OnSwitch(bool _value)
        {
            base.OnSwitch(_value);
            SoundSystem.Instance.ToggleSound();
            SoundSystem.Instance.PlayUIClick();
            VibrationSystem.Instance.PlayVibration();
        }
    }
}