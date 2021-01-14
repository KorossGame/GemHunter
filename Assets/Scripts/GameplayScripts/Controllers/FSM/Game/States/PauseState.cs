using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseState : GameState
{
    public PauseState(GameFSM FSM) : base(FSM)
    {
    }

    public override IEnumerator Enter()
    {
        PauseMenu.instance.gameObject.SetActive(true);
        Time.timeScale = 0;
        yield break;
    }

    public override IEnumerator Exit()
    {
        PauseMenu.instance.gameObject.SetActive(false);
        Time.timeScale = 1;
        yield break;
    }

    public override IEnumerator Play()
    {
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
        {
            gameFSMObject.changeState(new PlayState(gameFSMObject));
        }
        yield break;
    }
}
