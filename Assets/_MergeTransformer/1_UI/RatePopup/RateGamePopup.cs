using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VTLTools;

namespace MergeAR.UI.RateGamePopup
{
    public class RateGamePopup : PopupBase
    {
        [SerializeField] List<StarButton> starButtons;

        public void Rate(int _value)
        {
            for (int i = 0; i <= _value; i++)
            {
                starButtons[i].IsRated = true;
            }
            if (_value == starButtons.Count - 1)
            {
                //CC_Interface.current.RateInApp();
            }

            foreach (StarButton button in starButtons)
            {
                button.SetInteractable(false);
            }
            StaticVariables.IsRated = true;
            StartCoroutine(DelayHidePopup());

            SoundSystem.Instance.PlayUIClick();
            VibrationSystem.Instance.PlayVibration();
        }
        IEnumerator DelayHidePopup()
        {
            yield return new WaitForSeconds(0f);
            this.Hide();
        }
    }
}