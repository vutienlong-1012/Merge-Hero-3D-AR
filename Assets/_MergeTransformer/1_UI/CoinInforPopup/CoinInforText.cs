using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MergeAR.UI
{
    public class CoinInforText : MonoBehaviour
    {
        Text coinText;
        [SerializeField]
        Text CoinText
        {
            get
            {
                if (coinText == null)
                    coinText = GetComponent<Text>();
                return coinText;
            }
        }

        [SerializeField, ReadOnly] bool isScaling = false;
        public void SetText(string _value)
        {
            CoinText.text = _value;

            if (isScaling)
                return;

            isScaling = true;
            this.transform.DOScale(1.3f, 0.04f).SetLoops(2, LoopType.Yoyo).OnComplete(() =>
            {
                isScaling = false;
            });
        }
    }
}