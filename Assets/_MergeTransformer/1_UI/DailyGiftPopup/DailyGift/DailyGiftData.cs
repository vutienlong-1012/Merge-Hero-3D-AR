using BreakInfinity;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MergeAR.UI.DailyGiftPopup
{
    [Serializable]
    public class DailyGiftData
    {
        [LabelWidth(40)]
        [HorizontalGroup("1"), Range(1, 7)]
        public int day = 1;

        [LabelWidth(40)]
        [HorizontalGroup("1")] public BigDouble value;
    }
}