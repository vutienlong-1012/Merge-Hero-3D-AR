using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MergeAR.Loading
{
    public class LoadingTextAnimation : MonoBehaviour
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

        private void Start()
        {
            StartCoroutine(AnimateLoadingText());
        }

        private IEnumerator AnimateLoadingText()
        {
            while (true)
            {
                _AddTextWithDot(1);
                yield return new WaitForSeconds(0.3f);
                _AddTextWithDot(2);
                yield return new WaitForSeconds(0.3f);
                _AddTextWithDot(3);
                yield return new WaitForSeconds(0.3f);
            }

            void _AddTextWithDot(int _dotNumber)
            {
                ThisText.text = I2.Loc.ScriptLocalization.LOADING;
                for (int i = 0; i < _dotNumber; i++)
                {
                    ThisText.text += ".";
                }

                try
                {
                    ThisText.text = (ThisText.text).ToUpper();
                }
                catch
                {

                }
            }
        }
    }
}