using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VTLTools;

namespace MergeAR.UI
{
    public class GraphicToggleGroup : MonoBehaviour
    {
        [SerializeField] Toggle lowGraphicToggle;
        [SerializeField] Toggle performantGraphicToggle;

        private void OnEnable()
        {
            switch (StaticVariables.SavedGraphicPreset)
            {
                case GraphicPreset.Performant:
                    performantGraphicToggle.isOn = true;
                    break;
                case GraphicPreset.Low:
                    lowGraphicToggle.isOn = true;
                    break;
            }

            lowGraphicToggle.onValueChanged.AddListener(ChangeLowGraphicPreset);
            performantGraphicToggle.onValueChanged.AddListener(ChangePerformantGraphicToggle);
        }
        private void OnDisable()
        {
            lowGraphicToggle.onValueChanged.RemoveListener(ChangeLowGraphicPreset);
            performantGraphicToggle.onValueChanged.RemoveListener(ChangePerformantGraphicToggle);
        }
        private void ChangePerformantGraphicToggle(bool _isOn)
        {
            if (_isOn)
                GraphicSystem.Instance.ChangeGraphicPreset(GraphicPreset.Performant);
        }

        private void ChangeLowGraphicPreset(bool _isOn)
        {
            if (_isOn)
                GraphicSystem.Instance.ChangeGraphicPreset(GraphicPreset.Low);
        }
    }
}