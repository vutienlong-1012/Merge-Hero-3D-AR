using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MergeAR.UI
{
    public class CanvasNativeAds : MonoBehaviour
    {
        Canvas canvas;
        [ShowInInspector]
        Canvas ThisCanvas
        {
            get
            {
                if (canvas == null)
                    canvas = GetComponent<Canvas>();
                return canvas;
            }
        }

        public void SetCameraCanvas(Camera _cam)
        {
            ThisCanvas.worldCamera = _cam;
        }
    }
}