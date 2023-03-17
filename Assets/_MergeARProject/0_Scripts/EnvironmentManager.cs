using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VTLTools;

namespace MergeAR
{
    public class EnvironmentManager : Singleton<EnvironmentManager>
    {
        [SerializeField]public GameObject environment;

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