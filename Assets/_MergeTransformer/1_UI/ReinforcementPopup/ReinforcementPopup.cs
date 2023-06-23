using Sirenix.OdinInspector;
using System;
using UnityEngine;
using VTLTools;

namespace MergeAR.UI.ReinforcementPopup
{
    public class ReinforcementPopup : PopupBase
    {
        [SerializeField, BoxGroup("Popup Reference")] SkinnedMeshRenderer heroSkinnedMeshRenderer;
        [SerializeField, BoxGroup("Popup Reference")] WatchAdsAndGetReinforcementButton watchAdsButton;
        [SerializeField, ReadOnly] CharacterData characterData;
        protected override void ButtonAddListener()
        {
            base.ButtonAddListener();
            closeButton.onClick.AddListener(ContinueSuggestReinforcement);
        }


        protected override void ButtonRemoveListener()
        {
            base.ButtonRemoveListener();
            closeButton.onClick.RemoveListener(ContinueSuggestReinforcement);
        }
        public override void Show(object _data = null, float _delay = 0, Action _actionOnStartShow = null, Action _actionOnCompleteShow = null, Action _actionOnStartHide = null, Action _actionOnCompleteHide = null)
        {
            base.Show(_data, _delay, _actionOnStartShow, _actionOnCompleteShow, _actionOnStartHide, _actionOnCompleteHide);

            characterData = (CharacterData)_data;
            watchAdsButton.Init(characterData);

            switch (characterData.Type)
            {
                case CharacterType.FriendlyMelee:
                    characterData = CharacterDataManager.Instance.GetCharacterDataByID(CharacterID.HM);
                    break;
                case CharacterType.FriendlyRanged:
                    characterData = CharacterDataManager.Instance.GetCharacterDataByID(CharacterID.HR);
                    break;
            }

            heroSkinnedMeshRenderer.sharedMesh = characterData.mesh;
            heroSkinnedMeshRenderer.material = characterData.material;
        }

        private void ContinueSuggestReinforcement()
        {
            Reinforcement.Instance.Play();
        }

    }
}