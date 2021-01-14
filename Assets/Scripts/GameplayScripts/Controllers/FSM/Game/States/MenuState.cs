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
        if (Spawner.instance)
        {
            // Kill all enemies and set spawner inactive
            Spawner.instance.active = false;
            Spawner.instance.StartCoroutine(Spawner.instance.KillAllEnemies());
            Spawner.instance.ResetWaveNumber();
        }

        if (PlayerManager.instance && PlayerManager.instance.player)
        {
            PlayerManager.instance.player.GetComponent<Player>().ChangeAnimationState("Die");
        }

        // If player exits from level there is no main menu music playing
        if (!AudioManager.instance.checkIfPlaying("MainMenuMusic"))
        {
            AudioManager.instance.PlaySound("MainMenuMusic");
        }

        // Load Menu scene
        SceneManager.LoadScene("Menu");
        return base.Enter();
    }

    public override IEnumerator Exit()
    {
        AudioManager.instance.StopSound("MainMenuMusic");
        return base.Exit();
    }
}
