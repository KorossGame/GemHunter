using System.Collections;
using UnityEngine;

public abstract class GameState : State
{
    protected GameFSM gameFSMObject;

    // public constructor which will pass variables to each of the states
    public GameState(GameFSM FSM)
    {
        gameFSMObject = FSM;
    }

    public virtual IEnumerator Play()
    {
        yield break;
    }
}