using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartState : GameState
{
    public RestartState(GameFSM FSM) : base(FSM)
    {
    }

    public override IEnumerator Enter()
    {
        // Delete all the bullets
        GameObject[] bullets = GameObject.FindGameObjectsWithTag("Bullet");
        foreach (GameObject enemyObject in bullets)
        {
            Object.Destroy(enemyObject);
        }

        // Load menu scene
        SceneManager.LoadScene("Menu");
        Game.instance.stateMachine.changeState(new MenuState(Game.instance.stateMachine));

        return base.Enter();
    }
}
