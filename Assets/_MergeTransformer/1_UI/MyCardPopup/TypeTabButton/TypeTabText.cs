using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MergeAR.UI.MyCardPopup
{
    public class TypeTabText : MonoBehaviour
    {
        Text text;
        [ShowInInspector]
        Text ThisText
        {
            get
            {
                if (text == null)
                    text = GetComponent<Text>();
                return text;
            }
        }

        Outline[] outlines;

        [ShowInInspector]
        Outline[] Outlines
        {
            get
            {
                if (outlines == null)
                    outlines = GetComponents<Outline>();
                return outlines;
            }
        }

        [SerializeField] Color showColor;
        [SerializeField] Color hideColor;

        public void SetShow(bool _isShow)
        {
            if (_isShow)
            {
                ThisText.DOColor(showColor, 0.3f);
                foreach (Outline outline in Outlines)
                {
                    outline.DOFade(1, 0.3f);
                }
            }
            else
            {
                ThisText.DOColor(hideColor, 0.3f);
                foreach (Outline outline in Outlines)
                {
                    outline.DOFade(0, 0.3f);
                }
            }
        }
    }
}