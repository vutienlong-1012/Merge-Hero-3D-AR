using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace MergeAR.UI.MergingPopup
{
    public class FightButton : MonoBehaviour
    {
        Button button;

        [ShowInInspector, ReadOnly]
        Button Button
        {
            get
            {
                if (button != null)
                {
                    return button;
                }
                else
                {
                    button = this.GetComponent<Button>();
                    return button;
                }
            }
        }

        private void OnEnable()
        {
            Button.onClick.AddListener(OnClickFightButton);
        }

        private void OnDisable()
        {
            Button.onClick.RemoveListener(OnClickFightButton);
        }

        void OnClickFightButton()
        {
            SoundSystem.Instance.PlayUIClick();
            VibrationSystem.Instance.PlayVibration();
            GameManager.Instance.State = VTLTools.GameState.Fight;
        }
    }
}