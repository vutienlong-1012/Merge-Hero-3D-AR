using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VTLTools;

namespace MergeAR.UI.MyCardPopup
{
    public class MyCardPopup : PopupBase
    {
        [SerializeField, BoxGroup("Popup Reference")] TypeTabButton meleeTypTabButton;
        [SerializeField, BoxGroup("Popup Reference")] TypeTabButton rangedTypTabButton;
        [SerializeField, BoxGroup("Popup Reference")] Transform cardPlacement;

        [SerializeField] HeroCard heroCardPrefab;

        [Button]
        public override void Show(object _data = null, float _delay = 0, Action _actionOnStartShow = null, Action _actionOnCompleteShow = null, Action _actionOnStartHide = null, Action _actionOnCompleteHide = null)
        {
            base.Show(_data, _delay, _actionOnStartShow, _actionOnCompleteShow, _actionOnStartHide, _actionOnCompleteHide);
            Init();
            ControlManager.Instance.isAllowToMerge = false;
        }

        public override void Hide()
        {
            base.Hide();
            ControlManager.Instance.isAllowToMerge = true;
        }

        public override void Init()
        {
            base.Init();
            SpawnAllHeroCards(CharacterType.FriendlyMelee);
        }

        public void SpawnAllHeroCards(CharacterType _charType)
        {
            Helpers.DestroyAllChilds(cardPlacement);


            switch (_charType)
            {
                case CharacterType.FriendlyMelee:
                    meleeTypTabButton.IsChoosing = true;
                    rangedTypTabButton.IsChoosing = false;
                    break;
                case CharacterType.FriendlyRanged:
                    meleeTypTabButton.IsChoosing = false;
                    rangedTypTabButton.IsChoosing = true;
                    break;
            }


            foreach (var item in CharacterDataManager.Instance.allCharacterDatas)
            {
                if (item.iD == CharacterID.HR || item.iD == CharacterID.HM)
                    continue;

                if (item.Type == _charType)
                {
                    HeroCard _heroCard = Instantiate(heroCardPrefab, cardPlacement);
                    _heroCard.Init(item);
                }

            }
        }
    }
}