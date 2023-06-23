using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MergeAR
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] Slider mainSlider;
        [SerializeField] Slider subSlider;
        [SerializeField] Image filterImage;

        [SerializeField, ReadOnly] bool isBlinking = false;
        [SerializeField] float effectTime = 0.2f;

        [Button]
        public void Init(float _maxValue)
        {
            mainSlider.maxValue = _maxValue;
            mainSlider.value = mainSlider.maxValue;

            subSlider.maxValue = _maxValue;
            subSlider.value = mainSlider.maxValue;
        }

        [Button]
        public void UpdateSlider(float _value)
        {
            mainSlider.DOValue(_value, effectTime).OnComplete(() =>
            {
                subSlider.DOValue(_value, effectTime);
            });

            if (!isBlinking)
            {
                isBlinking = true;
                filterImage.DOColor(new Color(1, 1, 1, 0.5f), effectTime / 2).SetLoops(2, LoopType.Yoyo);
                this.transform.DOScale(this.transform.localScale * 1.1f, effectTime / 2).SetLoops(2, LoopType.Yoyo).OnComplete(() =>
                {
                    isBlinking = false;
                });
            }
        }
    }
}

