using DG.Tweening;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using VTLTools;

namespace MergeAR.UI.CheatPopup
{
    public class CheatPopup : PopupBase
    {
        [SerializeField, BoxGroup("Popup Reference")] Toggle fpsToggle;
        [SerializeField, BoxGroup("Popup Reference")] Toggle debugToggle;
        [SerializeField, BoxGroup("Popup Reference")] Toggle mediationToggle;
        [SerializeField, BoxGroup("Popup Reference")] Toggle hideUiToggle;
        [SerializeField, BoxGroup("Popup Reference")] Toggle noAdToggle;

        [SerializeField, BoxGroup("Popup Reference")] Button hackCoinButton;

        [SerializeField] GameObject fpsPopup;
        [SerializeField] GameObject debugPopup;


        protected override void ButtonAddListener()
        {
            base.ButtonAddListener();
            fpsToggle.onValueChanged.AddListener(SetActiveFpsPopup);
            debugToggle.onValueChanged.AddListener(SetActiveDebugPopup);
            hackCoinButton.onClick.AddListener(TossCoinToFace);
            hideUiToggle.onValueChanged.AddListener(SetUIActive);
        }



        protected override void ButtonRemoveListener()
        {
            base.ButtonRemoveListener();
            fpsToggle.onValueChanged.RemoveListener(SetActiveFpsPopup);
            debugToggle.onValueChanged.RemoveListener(SetActiveDebugPopup);
            hackCoinButton.onClick.RemoveListener(TossCoinToFace);
            hideUiToggle.onValueChanged.RemoveListener(SetUIActive);
        }

        private void SetActiveFpsPopup(bool _value)
        {
            fpsPopup.SetActive(_value);
        }

        private void SetActiveDebugPopup(bool _value)
        {
            debugPopup.SetActive(_value);
        }

        private void TossCoinToFace()
        {
            UIManager.Instance.coinInforPopup.SpawnAndPlusCoins(hackCoinButton.transform, 1000000000, null);
        }

        private void SetUIActive(bool _value)
        {
            //List<Image> _allImage = Helpers.GetAllChildsComponent<Image>(this.transform.parent);
            //List<Text> _allText = Helpers.GetAllChildsComponent<Text>(this.transform.parent);    
            Image[] _allImage = FindObjectsOfType<Image>(true);
            Text[] _allText = FindObjectsOfType<Text>(true);
            HealthBar[] _allHealthBar = FindObjectsOfType<HealthBar>(true);
            switch (_value)
            {
                case true:
                    foreach (var item in _allImage)
                    {
                        item.DOFade(0, 0f);
                    }
                    foreach (var item in _allText)
                    {

                        item.DOFade(0, 0f);
                    }
                    UIManager.Instance.HidePopup(UIManager.Instance.cheatPopup);
                    UIManager.Instance.HidePopup(UIManager.Instance.settingPopup);

                    foreach (var item in _allHealthBar)
                    {
                        List<Image> _allImageInBar = Helpers.GetAllChildsComponent<Image>(item.transform);
                        foreach (var image in _allImageInBar)
                        {
                            if (image.name != "FilterImage")
                                image.DOFade(1, 0);
                        }
                    }

                    break;
                case false:
                    break;
            }
        }
    }
}