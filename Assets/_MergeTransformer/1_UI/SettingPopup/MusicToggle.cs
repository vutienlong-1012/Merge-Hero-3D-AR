using UnityEngine;
using UnityEngine.UI;
using VTLTools;

namespace MergeAR.UI
{
    public class MusicToggle : SettingToggle
    {
        protected override void OnEnable()
        {
            toggle.isOn = StaticVariables.IsMusicOn;
            base.OnEnable();
        }
        protected override void OnDisable()
        {
            base.OnDisable();
        }

        protected override void OnSwitch(bool _value)
        {
            base.OnSwitch(_value);
            MusicSystem.Instance.ToggleMusic();
            SoundSystem.Instance.PlayUIClick();
            VibrationSystem.Instance.PlayVibration();
        }
    }
}