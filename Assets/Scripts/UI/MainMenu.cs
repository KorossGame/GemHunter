using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject settingsObject;
    private string currentState;

    public void StartGame()
    {
        PlayClickSound();
        SceneManager.LoadScene("LoadingScreen");
        Game.instance.stateMachine.changeState(new PlayState(Game.instance.stateMachine));
    }

    public void EnterSettings()
    {
        PlayClickSound();
        ChangeAnimationState("OpenSettings");
    }

    public void ExitSettings()
    {
        PlayClickSound();
        ChangeAnimationState("CloseSettings");
    }

    public void EnterCheats()
    {
        PlayClickSound();
        ChangeAnimationState("OpenCheats");
    }

    public void ExitCheats()
    {
        PlayClickSound();
        ChangeAnimationState("CloseCheats");
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

    void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;
        animator.Play(newState);
        currentState = newState;
    }
}
