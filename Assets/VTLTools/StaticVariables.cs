using I2.Loc;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VTLTools
{
    public class StaticVariables : MonoBehaviour
    {
        [ShowInInspector]
        public static bool IsSoundOn
        {
            get => VTLPlayerPrefs.GetBool(StringsSafeAccess.PREF_IS_SOUND_ON, true);
            set => VTLPlayerPrefs.SetBool(StringsSafeAccess.PREF_IS_SOUND_ON, value);
        }

        [ShowInInspector]
        public static bool IsMusicOn
        {
            get => VTLPlayerPrefs.GetBool(StringsSafeAccess.PREF_IS_MUSIC_ON, true);
            set => VTLPlayerPrefs.SetBool(StringsSafeAccess.PREF_IS_MUSIC_ON, value);
        }

        [ShowInInspector]
        public static bool IsVibrationOn
        {
            get => VTLPlayerPrefs.GetBool(StringsSafeAccess.PREF_IS_VIBRATION_ON, true);
            set => VTLPlayerPrefs.SetBool(StringsSafeAccess.PREF_IS_VIBRATION_ON, value);
        }

        [ShowInInspector]
        public static string CurrentLanguage
        {
            get => LocalizationManager.CurrentLanguage;
            set => LocalizationManager.CurrentLanguage = value;
        }
    }
}