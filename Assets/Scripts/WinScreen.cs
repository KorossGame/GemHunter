using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreen : MonoBehaviour
{
    [SerializeField] private GameObject congratulationMessage;
    [SerializeField] private GameObject toBeContinueMessage;

    private bool activated = false;

    private void Awake()
    {
        if (PlayerManager.instance.player)
        {
            PlayerManager.instance.player.SetActive(false);
        }
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            if (!activated)
            {
                activated = true;
                congratulationMessage.SetActive(false);
                toBeContinueMessage.SetActive(true);
            }
            else
            {
                SceneManager.LoadScene("Menu");
                Game.instance.stateMachine.changeState(new MenuState(Game.instance.stateMachine));
            }
        }
    }

}
