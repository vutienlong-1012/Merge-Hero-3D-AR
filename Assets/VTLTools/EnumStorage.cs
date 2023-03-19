using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VTLTools
{
    [Serializable]
    public enum GameState
    {
        None,
        ResetRound,
        Starting,
        WaitToPlaceEnvironment,
        Idle,
        Lose,
        Win,
        Retry,
    }

    public enum MenuItemState
    {
        None,
        Showing,
        Showed,
        Hiding,
        Hidden,
    }

    public enum GridState
    {
        None,
        Empty,
        Waiting,
        Chosen,
    }

}