using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VTLTools;

namespace MergeAR.UI.CheatPopup
{
    public class MediationToggle : MonoBehaviour
    {
        Toggle toggle;
        [ShowInInspector]
        Toggle ThisToggle
        {
            get
            {
                if (toggle == null)
                    toggle = GetComponent<Toggle>();
                return toggle;
            }
        }

        private void OnEnable()
        {
            ThisToggle.isOn = StaticVariables.IsShowMediation;
            ThisToggle.onValueChanged.AddListener(OnClickToggle);
        }

        private void OnDisable()
        {
            ThisToggle.onValueChanged.RemoveListener(OnClickToggle);
        }

        private void OnClickToggle(bool _value)
        {
            StaticVariables.IsShowMediation = _value;
        }
    }
}