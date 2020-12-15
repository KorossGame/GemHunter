using System.Collections;
using UnityEngine;

public class LoseState : GameState
{

    public LoseState() : base()
    {

    }

    public override IEnumerator Enter()
    {
        Debug.Log("You lost! Try once more?");
        yield break;
    }
}
