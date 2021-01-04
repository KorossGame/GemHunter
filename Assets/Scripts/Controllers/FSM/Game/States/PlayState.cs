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
        
        // If player pause the game we dont need to load game managers again
        if (!Game.instance.loadedGameManagers)
        {
            // Add game managers as empty childs of game controller
            GameObject.Instantiate(Resources.Load("Prefabs/GameManagers/GunManager"), gameFSMObject.transform.position, Quaternion.identity, gameFSMObject.transform);
            GameObject.Instantiate(Resources.Load("Prefabs/GameManagers/PlayerManager"), gameFSMObject.transform.position, Quaternion.identity, gameFSMObject.transform);

            // Add supportive game managers
            GameObject.Instantiate(Resources.Load("Prefabs/GameManagers/PowerUPManager"), gameFSMObject.transform.position, Quaternion.identity, gameFSMObject.transform);
            GameObject.Instantiate(Resources.Load("Prefabs/GameManagers/AmmoManager"), gameFSMObject.transform.position, Quaternion.identity, gameFSMObject.transform);

            // Spawn Camera
            GameObject.Instantiate(Resources.Load("Prefabs/Game/MainCamera"), gameFSMObject.transform.position, Quaternion.identity, gameFSMObject.transform);
            Game.instance.loadedGameManagers = true;
        }

        yield break;
    }

    public override IEnumerator Exit()
    {
        // Change music theme
        AudioManager.instance.StopSound("MainThemeMusic");
        yield break;
    }

    public override IEnumerator Play()
    {
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
        {
            gameFSMObject.changeState(new PauseState(gameFSMObject));
        }
        yield break;
    }
}
