using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using VTLTools;
using Sirenix.OdinInspector;
using UnityEngine.Events;
using MergeAR.UI;

public class Effect : MonoBehaviour
{
    [SerializeField] StoppedParticleSystemScript stoppedParticleSystemScript;
    [SerializeField] bool isRecycleWhenStop = true;
    void Start()
    {
        if (isRecycleWhenStop)
            stoppedParticleSystemScript.AdActionToStoppedEvent(RecycleThis);
    }

    void RecycleThis()
    {
        ObjectPool.Recycle(this.gameObject);
    }

    public void Init(Vector3 _position, Quaternion _rotation)
    {
        this.transform.position = _position;
        this.transform.rotation = _rotation;
    }

}