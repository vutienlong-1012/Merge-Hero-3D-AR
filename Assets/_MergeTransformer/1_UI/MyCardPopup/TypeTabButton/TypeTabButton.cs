using DG.Tweening;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VTLTools;

namespace MergeAR.UI.MyCardPopup
{
    public class TypeTabButton : MonoBehaviour
    {
        bool isChoosing;
        [ShowInInspector]
        public bool IsChoosing
        {
            get
            {
                return isChoosing;
            }
            set
            {
                isChoosing = value;
                ChooseTab(value);
            }
        }

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

        RectTransform rectTransform;
        [ShowInInspector]
        RectTransform ThisRectTransform
        {
            get
            {
                if (rectTransform == null)
                    rectTransform = GetComponent<RectTransform>();
                return rectTransform;
            }
        }

        [SerializeField] Text typeText;
        [SerializeField] Image backGroundImage;

        [SerializeField] Vector3 hidePos;
        [SerializeField] Vector3 showPos;

        [SerializeField] TypeTabText typeTabText;
        [SerializeField] CharacterType characterType;

        private void OnEnable()
        {
            ThisButton.onClick.AddListener(ChooseThisTabButton);
        }


        private void OnDisable()
        {
            ThisButton.onClick.RemoveListener(ChooseThisTabButton);
        }

        private void ChooseThisTabButton()
        {
            SoundSystem.Instance.PlayUIClick();
            VibrationSystem.Instance.PlayVibration();
            UIManager.Instance.myCardPopup.SpawnAllHeroCards(characterType);
        }

        public void ChooseTab(bool _isChoosing)
        {
            if (_isChoosing)
            {
                ThisRectTransform.DOLocalMove(showPos, 0.3f);
                typeTabText.SetShow(true);

                backGroundImage.DOColor(Color.white, 0.3f);
            }
            else
            {
                ThisRectTransform.DOLocalMove(hidePos, 0.3f);
                typeTabText.SetShow(false);

                backGroundImage.DOColor(Color.gray, 0.3f);
            }
        }
    }
}