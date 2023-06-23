using VTLTools;
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
        public void ToggleVibration()
        {
            StaticVariables.IsVibrationOn = !StaticVariables.IsVibrationOn;
        }
    }
}