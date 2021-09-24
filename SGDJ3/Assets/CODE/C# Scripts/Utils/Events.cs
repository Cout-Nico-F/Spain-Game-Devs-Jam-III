using UnityEngine.Events;

public class Events
{
    [System.Serializable] public class EventLevelComplete : UnityEvent<bool> { }
    [System.Serializable] public class EventGameOver : UnityEvent<bool> { }
}
