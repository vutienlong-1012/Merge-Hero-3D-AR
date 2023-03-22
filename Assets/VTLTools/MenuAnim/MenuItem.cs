using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VTLTools.UIAnimation
{
    public abstract class MenuItem : MonoBehaviour
    {
        [Button, BoxGroup("Time setting")] public float DelayShow;
        [Button, BoxGroup("Time setting")] public float DelayHide;
        [Button, BoxGroup("Time setting")] public float TimeShow;
        [Button, BoxGroup("Time setting")] public float TimeHide;

        [ShowInInspector, ReadOnly] public MenuItemState ThisMenuItemState { get; protected set; }

        public abstract void StartShow();
        public abstract IEnumerator IEStartShow();
        public abstract void StartHide();
        public abstract IEnumerator IEStartHide();

        [Button, BoxGroup("Set Position")]
        public abstract void SetThisAsShow();
        [Button, BoxGroup("Set Position")]
        public abstract void SetThisAsHide();
        [Button, BoxGroup("Preview Position")]
        public abstract void PreviewShow();
        [Button, BoxGroup("Preview Position")]
        public abstract void PreviewHide();

    }

}