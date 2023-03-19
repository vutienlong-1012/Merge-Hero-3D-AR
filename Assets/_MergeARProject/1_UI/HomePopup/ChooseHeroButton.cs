using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MergeAR.UI
{
    public class ChooseHeroButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [ShowInInspector, ReadOnly]
        public bool IsHolding { get; private set; }


        //Detect current clicks on the GameObject (the one with the script attached)
        public void OnPointerDown(PointerEventData pointerEventData)
        {
            IsHolding = true;
            ControlManager.Instance.PickUpCharacter();
        }

        //Detect if clicks are no longer registering
        public void OnPointerUp(PointerEventData pointerEventData)
        {
            IsHolding = false;
            ControlManager.Instance.ReleaseCharacter();
        }
    }
}