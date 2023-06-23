using DG.Tweening;
using I2.Loc.SimpleJSON;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MergeAR.UI
{
    public class SettingToggle : MonoBehaviour
    {
        [SerializeField] protected Toggle toggle;
        [SerializeField] RectTransform checkMarkRectTransform;
        [SerializeField] Image checkMarkImage;
        [SerializeField] Image backgroundImage;
        [SerializeField, ReadOnly] Vector2 startHandlePosition;
        [SerializeField] Sprite backgroundToggleOnSprite;
        [SerializeField] Sprite backgroundToggleOffSprite;
        [SerializeField] Color handleColorOn;
        [SerializeField] Color handleColorOff;



        protected virtual void OnEnable()
        {
            startHandlePosition = new Vector3(-25, 0, 0);
            checkMarkRectTransform.anchoredPosition = toggle.isOn ? startHandlePosition * -1 : startHandlePosition;
            checkMarkImage.color = toggle.isOn ? handleColorOn : handleColorOff;
            toggle.onValueChanged.AddListener(OnSwitch);
        }

        protected virtual void OnDisable()
        {
            toggle.onValueChanged.RemoveListener(OnSwitch);
        }

        protected virtual void OnSwitch(bool _isOn)
        {
            checkMarkRectTransform.DOAnchorPos(_isOn ? startHandlePosition * -1 : startHandlePosition, .3f).SetEase(Ease.InOutBack);
            backgroundImage.sprite = _isOn ? backgroundToggleOnSprite : backgroundToggleOffSprite;

            if (_isOn)
                checkMarkImage.DOColor(handleColorOn, 0.3f);
            else
                checkMarkImage.DOColor(handleColorOff, 0.3f);
        }
    }
}