using System;

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
        Merging,
        Fight,
        DefeatBattle,
        VictoryBattle,
    }
    public enum MenuItemState
    {
        None,
        Showing,
        Showed,
        Hiding,
        Hidden,
    }
    public enum CharacterType
    {
        None,
        EnemyMelee,
        FriendlyMelee,
        EnemyRanged,
        FriendlyRanged,
    }
    public enum CharacterFaction
    {
        None,
        Enemy,
        Friendly,
    }
    public enum CharacterID
    {

        None,
        EM1, EM2, EM3, EM4, EM5, EM6, EM7, EM8, EM9, EM10,
        FM0, FM1, FM2, FM3, FM4, FM5, FM6, FM7, FM8, FM9, FM10, HM,
        ER1, ER2, ER3, ER4, ER5, ER6, ER7, ER8, ER9, ER10,
        FR1, FR2, FR3, FR4, FR5, FR6, FR7, FR8, FR9, FR10, HR,
    }
    public enum CharacterState
    {
        Idle,
        LookingForTarget,
        MoveToTarget,
        AttackTarget,
        Dead,
        Dance,
        Fall,
    }
    public enum KindOfChoice
    {
        None = 0,
        Plus = 1,
        Minus = 2,
        Multi = 3,
        Divine = 4,
    }
    public enum GroupFriendlyType
    {
        None,
        Type1,
        Type2,
        Type3,
        Type4,
        Type5,
        Type6,
        Type7,
    }
    public enum Position
    {
        None,
        left,
        middle,
        right,
    }

    public enum BuyButtonState
    {
        None,
        Enough,
        NotEnough,
        Free,
    }

    public enum OnlineRewardButtonState
    {
        None,
        Counting,
        Waiting,
        OutOfOrder
    }

    public enum ReinforcementType
    {
        None,
        Coin,
        HelpMelee,
        HelpRanged,
    }

    public enum GraphicPreset
    {
        Performant, Low,
    }

    public enum TutorialPhase
    {
        None,
        FirstBuyCharacter,
        FirstMergeCharacter,
        FirstMoveCharacter,
        FinishTutorial
    }

    public enum EnvironmentBackground
    {
        None,
        Space,
        City
    }

    public enum RoadType
    {
        None,
        Normal,
        PitLeft,
        PitRight,
        Pit6m,
        Pit10m,
    }
}