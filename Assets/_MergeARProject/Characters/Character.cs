using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MergeAR
{
    public class Character : MonoBehaviour
    {
        public void SetNewParentCharacter(Transform _parent, Vector3 _offset)
        {
            this.transform.parent = _parent;
            this.transform.DOLocalMove(_offset, 0.5f);
        }
    }
}