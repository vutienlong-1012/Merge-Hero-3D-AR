using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MergeAR.UI
{
    public class ScaleEnvironmentButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField, ReadOnly] bool isHolding = false;
        [SerializeField] float speed = 1.0f;
        [SerializeField] Vector3 scaleDirection;
        [SerializeField, ReadOnly] Transform environmentTransform;

        [SerializeField] float maxScale = 3;
        [SerializeField] float minScale = 0.2f;

        public void OnPointerDown(PointerEventData eventData)
        {
            isHolding = true;
            InvokeRepeating(nameof(ScaleEnvironment), 0f, Time.deltaTime);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            isHolding = false;
            CancelInvoke(nameof(ScaleEnvironment));
        }

        private void Start()
        {
            environmentTransform = EnvironmentManager.Instance.environment.transform;
        }

        Vector3 newScale;
        void ScaleEnvironment()
        {
            newScale = environmentTransform.localScale + speed * Time.deltaTime * scaleDirection;
            newScale = Vector3.Max(newScale, new Vector3(minScale, minScale, minScale));
            newScale = Vector3.Min(newScale, new Vector3(maxScale, maxScale, maxScale));

            environmentTransform.localScale = newScale;
        }
    }
}
