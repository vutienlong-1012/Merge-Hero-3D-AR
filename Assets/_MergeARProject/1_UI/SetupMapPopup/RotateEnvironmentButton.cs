using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MergeAR.UI
{
    public class RotateEnvironmentButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField, ReadOnly] bool isHolding = false;
        [SerializeField] float speed = 1.0f;
        [SerializeField] Vector3 rotateDirection;
        [SerializeField, ReadOnly] Transform environmentTransform;

        public void OnPointerDown(PointerEventData eventData)
        {
            isHolding = true;
            InvokeRepeating(nameof(RotateEnvironment), 0f, Time.deltaTime);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            isHolding = false;
            CancelInvoke(nameof(RotateEnvironment));
        }

        private void Start()
        {
            environmentTransform = EnvironmentManager.Instance.environment.transform;
        }

        void RotateEnvironment()
        {
            environmentTransform.transform.Rotate(speed * Time.deltaTime * rotateDirection);
        }
    }
}


