using System.Collections;
using System.Collections.Generic;
using VTLTools;
using UnityEngine;
using Lofelt.NiceVibrations;
using Sirenix.OdinInspector;

namespace MergeAR
{
    public class VibrationSystem : Singleton<VibrationSystem>
    {
        public void PlayVibration()
        {
            if (!StaticVariables.IsVibrationOn)
                return;
            HapticPatterns.PlayPreset(HapticPatterns.PresetType.LightImpact);
        }

        [Button]
        public void ToggeVibration()
        {
            StaticVariables.IsVibrationOn = !StaticVariables.IsVibrationOn;
        }
    }
}