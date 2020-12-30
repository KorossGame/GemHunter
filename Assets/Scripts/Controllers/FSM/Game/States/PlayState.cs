using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayState : GameState
{
    public PlayState(GameFSM FSM) : base(FSM)
    {
    }

    public override IEnumerator Enter()
    {
        // Change music theme
        AudioManager.instance.PlaySound("MainThemeMusic");

        // Wait while scene is being changed
        while (SceneManager.GetActiveScene().buildIndex != 1)
        {
            yield return null;
        }

        // Add game managers as empty childs of game controller
        GameObject.Instantiate(Resources.Load("Prefabs/GameManagers/GunManager"), gameFSMObject.transform.position, Quaternion.identity, gameFSMObject.transform);
        GameObject.Instantiate(Resources.Load("Prefabs/GameManagers/PlayerManager"), gameFSMObject.transform.position, Quaternion.identity, gameFSMObject.transform);

        // Spawn Camera
        GameObject.Instantiate(Resources.Load("Prefabs/Game/MainCamera"), gameFSMObject.transform.position, Quaternion.identity, gameFSMObject.transform);
        yield break;
    }
}
