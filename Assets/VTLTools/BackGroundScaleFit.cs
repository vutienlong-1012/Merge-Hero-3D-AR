using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VTLTools
{
    public class BackGroundScaleFit : MonoBehaviour
    {
        [SerializeField] RectTransform bgRectTransform;

        public float resolutionX;
        public float resolutionY;

        public float ratioX;
        public float ratioY;

        private void Awake()
        {
            resolutionX = Screen.currentResolution.width;
            resolutionY = Screen.currentResolution.height;
        }

        private void Start()
        {
            ratioX = resolutionX / 720;
            ratioY = resolutionY / 1280;

            if (ratioX > ratioY)
            {

                //bgRectTransform.sizeDelta = bgRectTransform.sizeDelta / (1280 / 720) * (resolutionY / resolutionX);
                float _newX = bgRectTransform.sizeDelta.x / (720f / 1280f) * (resolutionX / resolutionY);
                float _newY = bgRectTransform.sizeDelta.y / (720f / 1280f) * (resolutionX / resolutionY);
                Debug.Log(new Vector2(_newX, _newY));
                bgRectTransform.sizeDelta = new Vector2(_newX, _newY);
                //loadingSlider.GetComponent<RectTransform>().localScale *= ratioX;
            }
            else
            {
                float _newX = bgRectTransform.sizeDelta.x / (1280f / 720f) * (resolutionY / resolutionX);
                float _newY = bgRectTransform.sizeDelta.y / (1280f / 720f) * (resolutionY / resolutionX);
                bgRectTransform.sizeDelta = new Vector2(_newX, _newY);
                //loadingSlider.GetComponent<RectTransform>().localScale *= ratioY;
            }
        }
    }
}