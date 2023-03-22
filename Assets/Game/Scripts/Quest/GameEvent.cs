using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameEvent
{
    public string EventDescription;
}

public class ExampleGameEvent : GameEvent
{
    public string Example;

    public ExampleGameEvent(string name)
    {
        Example = name;
    }
}
