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
    public enum CharacterType
    {
        None,
        Melee,
        Ranged,
    }
    public enum CharacterID
    {
        None,
        FM1, FM2, FM3, FM4, FM5, FM6, FM7, FM8, FM9, FM10,
        EM1, EM2, EM3, EM4, EM5, EM6, EM7, EM8, EM9, EM10,
        FR1, FR2, FR3, FR4, FR5, FR6, FR7, FR8, FR9, FR10,
        ER1, ER2, ER3, ER4, ER5, ER6, ER7, ER8, ER9, ER10,
    }

}