using System.Collections;
using UnityEngine;

public class StartState : GameState
{
    public StartState() : base()
    {

    }

    public override IEnumerator Enter()
    {
        Debug.Log("Welcome to the game!");
        yield break;
    }
}
