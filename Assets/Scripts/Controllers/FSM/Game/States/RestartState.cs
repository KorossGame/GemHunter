using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartState : GameState
{
    public RestartState() : base()
    {
        // Kill all enemies and set spawner inactive
        Spawner.instance.active = false;
        Spawner.instance.KillAllEnemies();

        // Delete all the bullets
        GameObject[] bullets = GameObject.FindGameObjectsWithTag("Bullet");
        foreach (GameObject enemyObject in bullets)
        {
            Object.Destroy(enemyObject);
        }
    }
}
