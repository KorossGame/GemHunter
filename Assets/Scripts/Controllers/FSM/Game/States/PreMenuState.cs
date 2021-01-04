using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PreMenuState : GameState
{

    public PreMenuState(GameFSM FSM) : base(FSM)
    {
        
    }

    public override IEnumerator Enter()
    {
        AudioManager.instance.PlaySound("MainMenuMusic");
        return base.Enter();
    }

    public override IEnumerator Exit()
    {
        return base.Enter();
    }

    public override IEnumerator Play()
    {
        if (Input.anyKeyDown)
        {
            gameFSMObject.changeState(new MenuState(gameFSMObject));
        }
        return base.Play();
    }
}
