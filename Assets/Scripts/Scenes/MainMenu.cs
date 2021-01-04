using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{

    public void StartGame()
    {
        PlayClickSound();
        SceneManager.LoadScene("Tutorial");
        Game.instance.stateMachine.changeState(new PlayState(Game.instance.stateMachine));
    }

    public void Exit()
    {
        PlayClickSound();
        Application.Quit();
    }

    public void PlayClickSound()
    {
        AudioManager.instance.PlaySound("ClickSound");
    }
}
