using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VTLTools;
using static UnityEngine.Rendering.DebugUI;

namespace MergeAR
{
    public class GraphicSystem : Singleton<GraphicSystem>
    {
        private void OnEnable()
        {
            ChangeGraphicPreset(StaticVariables.SavedGraphicPreset);
        }

        public void ChangeGraphicPreset(GraphicPreset _graphicPreset)
        {
            StaticVariables.SavedGraphicPreset = _graphicPreset;
            QualitySettings.SetQualityLevel((int)_graphicPreset, true);

            switch (_graphicPreset)
            {
                case GraphicPreset.Performant:
                    Application.targetFrameRate = 60;
                    break;
                case GraphicPreset.Low:
                    Application.targetFrameRate = 30;
                    break;
            }

            Debug.Log("<color=yellow>Quality setting now:</color> " + _graphicPreset + ", " + "<color=yellow>Target framerate:</color> " + Application.targetFrameRate);
        }
    }
}