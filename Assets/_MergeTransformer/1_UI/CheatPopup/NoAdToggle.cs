using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;
using VTLTools;

namespace MergeAR.UI.CheatPopup
{
    public class NoAdToggle : MonoBehaviour
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
            ThisToggle.onValueChanged.AddListener(OnClickToggle);
        }

        private void OnDisable()
        {
            ThisToggle.onValueChanged.RemoveListener(OnClickToggle);
        }

        private void OnClickToggle(bool _isOn)
        {
            StaticVariables.isHackNoAd = _isOn;
            if (_isOn)
            {
                //CC_Interface.current.DestroyBanner();
            }
        }
    }
}