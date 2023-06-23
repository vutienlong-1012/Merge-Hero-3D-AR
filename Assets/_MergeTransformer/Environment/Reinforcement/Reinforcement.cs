using BreakInfinity;
using DG.Tweening;
using MergeAR.UI;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using VTLTools;

namespace MergeAR
{
    public class Reinforcement : Singleton<Reinforcement>, IPointerClickHandler
    {
        [SerializeField] Transform modelPlacement;
        [SerializeField] DOTweenPath doTweenPath;

        [SerializeField] GameObject helpMeleePrefab;
        [SerializeField] GameObject helpRangedPrefab;
        [SerializeField] GameObject shipPrefab;
        CharacterData helpCharacterData;

        [SerializeField, ReadOnly] ReinforcementType reinforcementType;

        [SerializeField] GuildHand guildHand;

        bool isTouchable;
        [ShowInInspector, ReadOnly]
        public bool IsTouchable
        {
            get
            {
                return isTouchable;
            }
            set
            {
                isTouchable = value;
                guildHand.gameObject.SetActive(value);
            }
        }

        public void Init()
        {
            if (StaticVariables.loseStreak >= 3)
            {
                SpawnHelpGundam();
            }
            else
            {
                if (Helpers.RandomByWeight(new float[2] { 50, 50 }) == 0)
                {
                    SpawnShipCoin();
                }
                else
                    return;
            }

            Tween _tween = doTweenPath.GetTween();
            if (_tween != null)
            {
                _tween.OnStepComplete(CheckShouldContinueOrNot);
            }

            modelPlacement.gameObject.SetActive(true);
            doTweenPath.DOPlay();
            IsTouchable = true;
        }

        public void CheckShouldContinueOrNot()
        {
            if (GameManager.instance.State != GameState.Merging)
            {
                ImmediatelyDeActive();
            }
        }

        void SpawnShipCoin()
        {
            Helpers.DestroyAllChilds(modelPlacement);
            Instantiate(shipPrefab, modelPlacement);
            reinforcementType = ReinforcementType.Coin;
        }

        void SpawnHelpGundam()
        {
            Helpers.DestroyAllChilds(modelPlacement);

            helpCharacterData = CharacterDataManager.instance.GetHighestEnemyLevelMinusOne();

            if (helpCharacterData.Type == CharacterType.FriendlyMelee)
            {
                Instantiate(helpMeleePrefab, modelPlacement);
                reinforcementType = ReinforcementType.HelpMelee;
            }
            else
            {
                Instantiate(helpRangedPrefab, modelPlacement);
                reinforcementType = ReinforcementType.HelpRanged;
            }
        }

        public void Pause()
        {
            doTweenPath.DOPause();
        }

        public void Play()
        {
            doTweenPath.DOPlay();
        }

        public void ImmediatelyDeActive()
        {
            modelPlacement.gameObject.SetActive(false);
            doTweenPath.DORestart();
            doTweenPath.DOPause();

            // doTweenPath.onStepComplete.AddListener(CheckShouldContinueOrNot);

        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (!IsTouchable)
                return;

            doTweenPath.DOPause();
            switch (reinforcementType)
            {
                case ReinforcementType.Coin:
                    _ShowWantSomeCoinPopup();
                    break;
                case ReinforcementType.HelpMelee:
                    if (GridManager.instance.IsFriendlyGridFull())
                        _ShowWantSomeCoinPopup();
                    else
                        UIManager.instance.ShowPopup(UIManager.instance.reinforcementPopup, helpCharacterData);
                    break;
                case ReinforcementType.HelpRanged:
                    if (GridManager.instance.IsFriendlyGridFull())
                        _ShowWantSomeCoinPopup();
                    else
                        UIManager.instance.ShowPopup(UIManager.instance.reinforcementPopup, helpCharacterData);
                    break;
            }

            void _ShowWantSomeCoinPopup()
            {
                BigDouble _minReward = LevelReward.instance.GetRewardByLevel(StaticVariables.CurrentLevel - 1) * 4;
                BigDouble _maxReward = LevelReward.instance.GetRewardByLevel(StaticVariables.CurrentLevel + 1) * 6;

                double _random = Random.Range(0f, 1f);
                double _value = _minReward.ToDouble() + _random * (_maxReward - _minReward).ToDouble();


                UIManager.instance.ShowPopup(UIManager.instance.wantSomeCoinPopup, (BigDouble)_value);
            }
        }
    }
}