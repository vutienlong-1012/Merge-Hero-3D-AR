using DG.Tweening;
using MergeAR.UI.MergingPopup;
using Sirenix.OdinInspector;
using System;
using UnityEngine;
using UnityEngine.UI;
using VTLTools;

namespace MergeAR.UI
{
    public class FightPopUp : PopupBase
    {
        [SerializeField, BoxGroup("Popup Reference")] HealthBar totalEnemyHealthBar;
        [SerializeField, BoxGroup("Popup Reference")] HealthBar totalFriendlyHealthBar;
        [SerializeField, BoxGroup("Popup Reference")] SpeedUpButton speedUpButton;

        private void OnEnable()
        {
            if (StaticVariables.CurrentLevel < 2)
                speedUpButton.gameObject.SetActive(false);
            else
            {
                speedUpButton.gameObject.SetActive(true);
                speedUpButton.IsSpeedUp = false;
            }
        }

        public override void Show(object _data = null, float _delay = 0, Action _actionOnStartShow = null, Action _actionOnCompleteShow = null, Action _actionOnStartHide = null, Action _actionOnCompleteHide = null)
        {
            base.Show(_data, _delay, _actionOnStartShow, _actionOnCompleteShow, _actionOnStartHide, _actionOnCompleteHide);
            speedUpButton.SetInteractable(true);
        }

        public override void Hide()
        {
            base.Hide();
            speedUpButton.SetInteractable(false);
        }

        public override void Init()
        {
            base.Init();
            totalEnemyHealthBar.Init(CharacterDataManager.Instance.CurrentTotalEnemyHealth);
            totalFriendlyHealthBar.Init(CharacterDataManager.Instance.CurrentTotalFriendlyHealth);

        }

        public void UpdateSliderHealBar(VTLTools.CharacterFaction _faction)
        {
            switch (_faction)
            {
                case VTLTools.CharacterFaction.Enemy:
                    totalEnemyHealthBar.UpdateSlider(CharacterDataManager.Instance.CurrentTotalEnemyHealth);
                    break;
                case VTLTools.CharacterFaction.Friendly:
                    totalFriendlyHealthBar.UpdateSlider(CharacterDataManager.Instance.CurrentTotalFriendlyHealth);
                    break;
            }
        }
    }
}