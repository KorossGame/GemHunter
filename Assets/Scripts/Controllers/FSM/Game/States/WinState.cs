using System.Collections;
using UnityEngine;

public class WinState : GameState
{
    public WinState(): base()
    {

    }

    public override IEnumerator Enter()
    {
        Debug.Log("You Won! Congratulations!");
        yield break;
    }
}
