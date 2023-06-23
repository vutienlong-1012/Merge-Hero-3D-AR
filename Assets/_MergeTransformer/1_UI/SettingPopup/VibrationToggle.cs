using UnityEngine;
using UnityEngine.UI;
using VTLTools;

namespace MergeAR.UI
{
    public class VibrationToggle : SettingToggle
    {
        protected override void OnEnable()
        {
            toggle.isOn = StaticVariables.IsVibrationOn;
            base.OnEnable();
        }
        protected override void OnDisable()
        {
            base.OnDisable();
        }

        protected override void OnSwitch(bool _value)
        {
            base.OnSwitch(_value);
            VibrationSystem.Instance.ToggleVibration();
            SoundSystem.Instance.PlayUIClick();
            VibrationSystem.Instance.PlayVibration();
        }
    }
}