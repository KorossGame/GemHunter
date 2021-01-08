using System.Collections;
using UnityEngine;

public class WinState : GameState
{
    public WinState(GameFSM FSM) : base(FSM)
    {

    }

    public override IEnumerator Enter()
    {
        Debug.Log("You Won! Congratulations!");
        yield break;
    }
}
