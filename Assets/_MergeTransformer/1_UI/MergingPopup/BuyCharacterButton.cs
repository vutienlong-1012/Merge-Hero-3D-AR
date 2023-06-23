using BreakInfinity;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;
using VTLTools;

namespace MergeAR.UI.MergingPopup
{
    public class BuyCharacterButton : MonoBehaviour
    {
        [SerializeField]
        CharacterID characterID;

        [ShowInInspector]
        public CharacterType Type
        {
            get
            {
                return Helpers.DecideCharacterType(characterID);
            }
        }

        BuyButtonState buyButtonState;
        [ShowInInspector, ReadOnly]
        BuyButtonState BuyButtonState
        {
            get
            {
                return buyButtonState;
            }
            set
            {
                buyButtonState = value;
                SetButton();
            }
        }

        [ShowInInspector]
        BigDouble ValueToBuy
        {
            get
            {
                if (Type == CharacterType.FriendlyMelee)
                {
                    return CalculateButtonValue(StaticVariables.TotalNumberOfPurchasedMelee);
                }
                else
                {
                    return CalculateButtonValue(StaticVariables.TotalNumberOfPurchasedRanged);
                }
            }
        }

        Button button;

        [ShowInInspector, ReadOnly]
        Button ThisButton
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

        [SerializeField] Text valueText;
        [SerializeField] Image currencyImage;
        [SerializeField] Image characterImage;
        [SerializeField] Image[] bottomDecorImage;

        [SerializeField] Sprite coinImage;
        [SerializeField] Sprite adsImage;

        [SerializeField] AudioClip buyClip;

        [SerializeField] Color disableColor;

        private void OnEnable()
        {
            ThisButton.onClick.AddListener(OnClickBuyCharacter);
            EventDispatcher.Instance.AddListener(EventName.OnCoinValueChange, CheckState);
            EventDispatcher.Instance.AddListener(EventName.OnFriendlyGridFull, FullGridListener);
            CheckState();
        }


        private void OnDisable()
        {
            ThisButton.onClick.RemoveListener(OnClickBuyCharacter);
            EventDispatcher.Instance.RemoveListener(EventName.OnCoinValueChange, CheckState);
            EventDispatcher.Instance.RemoveListener(EventName.OnFriendlyGridFull, FullGridListener);
        }


        private void CheckState(EventName key = EventName.NONE, object data = null)
        {
            switch (Type)
            {
                case CharacterType.FriendlyMelee:
                    if (StaticVariables.ArchivedMeleeCharacterNumber > 0)
                    {
                        BuyButtonState = BuyButtonState.Free;
                        return;
                    }
                    break;
                case CharacterType.FriendlyRanged:
                    if (StaticVariables.ArchivedRangedCharacterNumber > 0)
                    {
                        BuyButtonState = BuyButtonState.Free;
                        return;
                    }
                    break;
            }

            if (ValueToBuy > StaticVariables.CurrentCoin)
            {
                BuyButtonState = BuyButtonState.NotEnough;
            }
            else
            {
                BuyButtonState = BuyButtonState.Enough;
            }
        }

        private void FullGridListener(EventName key, object data)
        {
            if ((bool)data == true)
            {
                ThisButton.interactable = false;
                _SetColorButton(disableColor);
            }
            else
            {
                ThisButton.interactable = true;
                _SetColorButton(Color.white);
            }

            void _SetColorButton(Color _color)
            {
                valueText.color = _color;
                currencyImage.color = _color;
                characterImage.color = _color;
                ThisButton.image.color = _color;

                foreach (var item in bottomDecorImage)
                {
                    item.color = _color;
                }
            }
        }

        private void OnClickBuyCharacter()
        {
            switch (BuyButtonState)
            {
                case BuyButtonState.Enough:
                    StaticVariables.CurrentCoin -= ValueToBuy;
                    //LogManager.LogResource(FlowType.sink, CurrencyType.Coin, (int)ValueToBuy.ToDouble(), "Buy" + Type, StaticVariables.CurrentLevel);
                    switch (Type)
                    {
                        case CharacterType.FriendlyMelee:
                            StaticVariables.TotalNumberOfPurchasedMelee++;
                            break;
                        case CharacterType.FriendlyRanged:
                            StaticVariables.TotalNumberOfPurchasedRanged++;
                            break;
                    }
                    CheckState();
                    _SuccessGetCharacter();
                    break;
                case BuyButtonState.NotEnough:
                        //CC_Interface.current.ShowRewardAds(0, "Reward_Get" + Type, StaticVariables.CurrentLevel, _CheckIsWatchAdSuccess);
                        //void _CheckIsWatchAdSuccess(bool _isWatched)
                        //{
                        //    if (_isWatched)
                        //        _SuccessGetCharacter();
                        //}
                    break;
                case BuyButtonState.Free:
                    switch (Type)
                    {
                        case CharacterType.FriendlyMelee:
                            StaticVariables.ArchivedMeleeCharacterNumber--;
                            break;
                        case CharacterType.FriendlyRanged:
                            StaticVariables.ArchivedRangedCharacterNumber--;
                            break;
                    }
                    CheckState();
                    _SuccessGetCharacter();
                    break;
            }

            void _SuccessGetCharacter()
            {
                if (StaticVariables.CurrentTutorialPhase == TutorialPhase.FirstBuyCharacter)
                {
                    StaticVariables.CurrentTutorialPhase++;
                    TutorialManager.Instance.HideTutorialFirstBuy();
                }
                TutorialManager.Instance.HideGuildHandTutorial();
                SoundSystem.Instance.PlaySoundOneShot(SoundSystem.Instance.uIAudioSource, buyClip);
                VibrationSystem.Instance.PlayVibration();
                CharacterDataManager.Instance.SpawnCharacterInGrid(GridManager.Instance.GetEmptyFriendlyGrid(), characterID);
                CharacterDataManager.Instance.SaveFriendlyGrids();
                GridManager.Instance.CheckFriendlyGridFull();
                EventDispatcher.Instance.Dispatch(EventName.OnBuyCharacter, characterID);
            }
        }

        void SetButton()
        {
            switch (BuyButtonState)
            {
                case BuyButtonState.Enough:
                    currencyImage.gameObject.SetActive(true);
                    currencyImage.sprite = coinImage;
                    valueText.text = BigDouble.ToText(ValueToBuy);

                    break;
                case BuyButtonState.NotEnough:
                    currencyImage.gameObject.SetActive(true);
                    currencyImage.sprite = adsImage;
                    valueText.text = I2.Loc.ScriptLocalization.FREE;
                    break;

                case BuyButtonState.Free:
                    currencyImage.gameObject.SetActive(false);
                    switch (Type)
                    {
                        case CharacterType.FriendlyMelee:
                            valueText.text = StaticVariables.ArchivedMeleeCharacterNumber.ToString();
                            break;
                        case CharacterType.FriendlyRanged:
                            valueText.text = StaticVariables.ArchivedRangedCharacterNumber.ToString();
                            break;
                    }
                    break;
            }
        }

        BigDouble CalculateButtonValue(int _numberPurchased)
        {
            if (_numberPurchased == 0)
                return 200;
            else
                return 200 + _numberPurchased * 605;
        }
    }
}