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
        // By default set set it off
        gameObject.SetActive(false);
    }

    public void ContinueGame()
    {
        Game.instance.stateMachine.changeState(new PlayState(Game.instance.stateMachine));
    }

    public void ExitToMenu()
    {
        SceneManager.LoadScene("Menu");
        Game.instance.stateMachine.changeState(new MenuState(Game.instance.stateMachine));
    }
}
