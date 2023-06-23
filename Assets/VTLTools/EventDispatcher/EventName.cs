using System.Collections.Generic;

public enum EventName
{
    NONE,

    OnGameStateChanged,

    OnHoldCharacter,
    OnReleaseCharacter,

    OnBuyCharacter,
    OnClearGrid,

    OnCoinValueChange,
    OnLevelChange,

    OnFriendlyGridFull,

    OnLoadBackground,
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
