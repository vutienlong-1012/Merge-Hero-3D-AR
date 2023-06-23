using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using VTLTools;

namespace MergeAR.UI.HomePopup
{
    public class SwipeToMoveButton : MonoBehaviour, IPointerDownHandler
    {
        public void OnPointerDown(PointerEventData eventData)
        {
            SoundSystem.Instance.PlayUIClick();
            VibrationSystem.Instance.PlayVibration();
            UIManager.Instance.HidePopup(UIManager.Instance.homePopup);
            //GameManager.Instance.State = GameState.Run;
        }
    }
}