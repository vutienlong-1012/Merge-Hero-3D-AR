using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VTLTools;

namespace MergeAR.UI
{
    public class RateUsButton : MonoBehaviour
    {
        Button button;
        [ShowInInspector]
        Button ThisButton
        {
            get
            {
                if (button == null)
                    button = GetComponent<Button>();
                return button;
            }
        }

        private void OnEnable()
        {
            ThisButton.onClick.AddListener(OnClickRateUs);
        }

        private void OnDisable()
        {
            ThisButton.onClick.RemoveListener(OnClickRateUs);
        }

        private void OnClickRateUs()
        {
            StaticVariables.IsRated = true;
            //CC_Interface.current.RateInApp();
            this.gameObject.SetActive(false);
            UIManager.Instance.settingPopup.ResizePopup();
        }
    }
}