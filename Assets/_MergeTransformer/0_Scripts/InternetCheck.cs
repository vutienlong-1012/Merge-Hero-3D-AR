using MergeAR.UI;
using UnityEngine;
using VTLTools;

public class InternetCheck : Singleton<InternetCheck>
{
    public void CheckInternetConnection()
    {
        NetworkReachability reachability = Application.internetReachability;

        if (reachability == NetworkReachability.NotReachable)
        {
            Debug.Log("<color=red>Device is NOT connected to the internet!</color>");
            if (!UIManager.Instance.noInternetPopup.IsShow)
                UIManager.Instance.ShowPopup(UIManager.Instance.noInternetPopup);
        }
        else
        {
            Debug.Log("<color=yellow>Device is connected to the internet.</color>");
            if (UIManager.Instance.noInternetPopup.IsShow)
                UIManager.Instance.HidePopup(UIManager.Instance.noInternetPopup);
        }
    }
}
