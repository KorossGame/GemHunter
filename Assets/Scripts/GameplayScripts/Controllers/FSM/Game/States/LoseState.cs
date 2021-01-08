using System.Collections;
using UnityEngine;

public class LoseState : GameState
{

    public LoseState(GameFSM FSM) : base(FSM)
    {

    }

    public override IEnumerator Enter()
    {
        Debug.Log("You lost! Try once more?");
        yield break;
    }
}
