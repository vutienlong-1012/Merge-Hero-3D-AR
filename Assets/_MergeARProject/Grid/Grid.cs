using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VTLTools;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using DG.Tweening;

namespace MergeAR
{
    [Serializable]
    public class Grid : MonoBehaviour
    {
        public Character currentCharacter;
        public GridState gridState;
    }
}

