using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu instance;

    void Awake()
    {
        instance = this;
    }

    public void ContinueGame()
    {
        Game.instance.stateMachine.changeState(new PlayState(Game.instance.stateMachine));
    }

    public void ExitToMenu()
    {
        Game.instance.stateMachine.changeState(new MenuState(Game.instance.stateMachine));
    }
}
