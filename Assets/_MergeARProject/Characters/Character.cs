using DG.Tweening;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using VTLTools;

namespace MergeAR
{
    [Serializable]
    public class Character : MonoBehaviour
    {
        [SerializeField]
        [OnValueChanged(nameof(LoadData))]
        public CharacterData data;
        [SerializeField] Transform modelPlacement;

        private void OnEnable()
        {
            LoadData();
        }
        void LoadData()
        {
            Helpers.DestroyAllChilds(modelPlacement.gameObject);
            GameObject _model = Instantiate(data.model, modelPlacement);
            _model.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
            _model.transform.localPosition = Vector3.zero;
        }
        public void SetNewParentCharacter(Transform _parent, Vector3 _offset)
        {
            this.transform.parent = _parent;
            this.transform.DOLocalMove(_offset, 0.5f);
            this.transform.DOLocalRotate(Vector3.zero, 0.5f);
        }
    }
}