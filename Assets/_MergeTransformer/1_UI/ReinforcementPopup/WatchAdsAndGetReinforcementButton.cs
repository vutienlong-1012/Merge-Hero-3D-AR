using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VTLTools;

namespace MergeAR.UI.ReinforcementPopup
{
    public class WatchAdsAndGetReinforcementButton : MonoBehaviour
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

        [SerializeField, ReadOnly] CharacterData characterType;
        [SerializeField] AudioClip buyClip;

        private void OnEnable()
        {
            ThisButton.interactable = true;
            ThisButton.onClick.AddListener(WatchAdsAndGetHelpChar);
        }
        private void OnDisable()
        {
            ThisButton.onClick.RemoveListener(WatchAdsAndGetHelpChar);
        }

        private void WatchAdsAndGetHelpChar()
        {
            //CC_Interface.current.ShowRewardAds(0, "Reward_GetReinforcement", StaticVariables.CurrentLevel, _CheckIsWatchAdSuccess);
            //void _CheckIsWatchAdSuccess(bool _isWatched)
            //{
            //    if (_isWatched)
            //        SuccessGetCharacter();
            //}
        }


        public void Init(CharacterData _charData)
        {
            characterType = _charData;
        }

        void SuccessGetCharacter()
        {
            SoundSystem.Instance.PlaySoundOneShot(SoundSystem.Instance.uIAudioSource, buyClip);
            VibrationSystem.Instance.PlayVibration();
            CharacterDataManager.Instance.SpawnHelpCharacter(characterType);
            CharacterDataManager.Instance.SaveFriendlyGrids();
            GridManager.Instance.CheckFriendlyGridFull();
            //EventDispatcher.Instance.Dispatch(EventName.OnBuyCharacter, characterID);
            ThisButton.interactable = false;
            UIManager.Instance.HidePopup(UIManager.Instance.reinforcementPopup);
            Reinforcement.Instance.ImmediatelyDeActive();
        }
    }
}