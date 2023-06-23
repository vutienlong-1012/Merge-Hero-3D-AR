using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VTLTools;

namespace MergeAR
{
    public class EnvironmentManager : Singleton<EnvironmentManager>
    {
        public GameObject environment;

        [ShowInInspector]
        public Vector3 EnvironmentScale
        {
            get => this.transform.localScale;
        }
        public void SetActiveEnvironment(bool _isActive)
        {
            environment.SetActive(_isActive);
        }

        public void SetPosEnvironment(Pose _pose)
        {
            environment.transform.SetPositionAndRotation(_pose.position, _pose.rotation);
        }
    }
}