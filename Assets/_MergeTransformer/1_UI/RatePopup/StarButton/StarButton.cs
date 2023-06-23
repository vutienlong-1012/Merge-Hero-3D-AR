using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MergeAR.UI.RateGamePopup
{
    public class StarButton : MonoBehaviour
    {
        bool isRated = false;
        [ShowInInspector]
        public bool IsRated
        {
            get
            {
                return isRated;
            }
            set
            {
                isRated = value;
                if (isRated)
                {
                    ThisImage.sprite = yellowStar;
                }
                else
                {
                    ThisImage.sprite = whiteStar;
                }
            }
        }

        Button thisButton;
        [ShowInInspector]
        Button ThisButton
        {
            get
            {
                if (thisButton == null)
                {
                    thisButton = GetComponent<Button>();
                }
                return thisButton;
            }
        }

        Image thisImage;
        [ShowInInspector]
        Image ThisImage
        {
            get
            {
                if (thisImage == null)
                {
                    thisImage = GetComponent<Image>();
                }
                return thisImage;
            }
        }

        [ShowInInspector] int StarIndex => this.transform.GetSiblingIndex();

        [SerializeField] Sprite yellowStar;
        [SerializeField] Sprite whiteStar;

        private void OnEnable()
        {
            ThisButton.onClick.AddListener(OnClickRate);
        }
        private void OnDisable()
        {
            ThisButton.onClick.RemoveListener(OnClickRate);
        }

        private void OnClickRate()
        {
            UIManager.Instance.rateGamePopup.Rate(StarIndex);
        }

        public void SetInteractable(bool _value)
        {
            ThisButton.interactable = _value;
        }
    }
}