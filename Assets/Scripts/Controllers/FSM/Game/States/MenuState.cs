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
        AudioManager.instance.PlaySound("MainMenuMusic");
        return base.Enter();
    }

    public override IEnumerator Exit()
    {
        AudioManager.instance.StopSound("MainMenuMusic");
        SceneManager.LoadScene(1);
        yield return new WaitForEndOfFrame();
    }
}
