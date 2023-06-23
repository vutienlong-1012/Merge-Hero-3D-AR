using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MergeAR
{
    public class GuildHand : MonoBehaviour
    {
        private void Update()
        {
            this.transform.LookAt(Camera.main.transform.position);
        }
    }
}