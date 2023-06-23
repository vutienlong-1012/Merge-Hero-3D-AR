using Sirenix.OdinInspector;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace MergeAR.UI
{
    public class StoppedParticleSystemScript : MonoBehaviour
    {
        ParticleSystem parentParticleSystem;

        [ShowInInspector]
        public ParticleSystem ParentParticleSystem1
        {
            get
            {
                if (parentParticleSystem == null)
                    parentParticleSystem = GetComponent<ParticleSystem>();
                return parentParticleSystem;
            }
        }

        Action particleSystemStopCallback;

        void Start()
        {
            var main = ParentParticleSystem1.main;
            main.stopAction = ParticleSystemStopAction.Callback;
        }

        public void AdActionToStoppedEvent(Action _callback)
        {
            particleSystemStopCallback += _callback;
        }

        void OnParticleSystemStopped()
        {
            particleSystemStopCallback?.Invoke();
        }
    }
}