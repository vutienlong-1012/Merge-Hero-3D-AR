using System.Collections.Generic;

public enum EventName
{
    NONE,

    OnAfterStateChanged,
    OnBeforeStateChanged,

    OnChangeSkin,
    OnChangColor,

    selectItemX,
    SetItemX,
    UpdateNotifyShop,
    UpdateNotifySpin,

    OnChooseNode,
    OnAfterChooseNode,

    OnBeforeBuildChanged,
    OnAfterBuildChanged,

    OnChosingBonusValue,

    OnMoveEnemy,

    OnChosenNation,

    OnBeforeFightStateChange,
    OnAfterFightStateChange,
    
    OnChangeScene,

    OnBossStartAttack,

    OnFixedUpdate,
}

public class EventTypeComparer : IEqualityComparer<EventName>
{
    public bool Equals(EventName x, EventName y)
    {
        return x == y;
    }

    public int GetHashCode(EventName t)
    {
        return (int)t;
    }
}
