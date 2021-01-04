using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuState : GameState
{

    public MenuState(GameFSM FSM) : base(FSM)
    {
        
    }

    public override IEnumerator Enter()
    {
        SceneManager.LoadScene("Menu");
        return base.Enter();
    }

    public override IEnumerator Exit()
    {
        AudioManager.instance.StopSound("MainMenuMusic");
        return base.Exit();
    }
}
