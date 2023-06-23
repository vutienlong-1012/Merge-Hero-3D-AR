using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MergeAR.UI
{
    public class NoInternetPopup : PopupBase
    {
        private void Update()
        {
            InternetCheck.Instance.CheckInternetConnection();
        }
    }
}