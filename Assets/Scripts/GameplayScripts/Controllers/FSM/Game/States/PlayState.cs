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

        if (PlayerManager.instance && !PlayerManager.instance.player)
        {
            GameObject.Instantiate(Resources.Load("Prefabs/Game/Player"), gameFSMObject.transform.position, Quaternion.identity);
        }

        // If player pause the game we dont need to load game managers again
        if (!Game.instance.loadedGameManagers)
        {
            GameObject.Instantiate(Resources.Load("Prefabs/Game/Player"), gameFSMObject.transform.position, Quaternion.identity);

            // Load Pause Menu UI
            GameObject.Instantiate(Resources.Load("Prefabs/UI/PauseMenu"), gameFSMObject.transform.position, Quaternion.identity, gameFSMObject.transform);

            // Add event manager
            GameObject.Instantiate(Resources.Load("Prefabs/GameManagers/CustomEventManager"), gameFSMObject.transform.position, Quaternion.identity, gameFSMObject.transform);

            // Add Grid manager
            GameObject.Instantiate(Resources.Load("Prefabs/GameManagers/GridManager"), gameFSMObject.transform.position, Quaternion.identity, gameFSMObject.transform);

            // Add player and gun managers
            GameObject.Instantiate(Resources.Load("Prefabs/GameManagers/GunManager"), gameFSMObject.transform.position, Quaternion.identity, gameFSMObject.transform);
            GameObject.Instantiate(Resources.Load("Prefabs/GameManagers/PlayerManager"), gameFSMObject.transform.position, Quaternion.identity, gameFSMObject.transform);

            // Add enemy game managers
            GameObject.Instantiate(Resources.Load("Prefabs/GameManagers/EnemyManager"), gameFSMObject.transform.position, Quaternion.identity, gameFSMObject.transform);

            // Add supportive game managers
            GameObject.Instantiate(Resources.Load("Prefabs/GameManagers/PowerUPManager"), gameFSMObject.transform.position, Quaternion.identity, gameFSMObject.transform);
            GameObject.Instantiate(Resources.Load("Prefabs/GameManagers/AmmoManager"), gameFSMObject.transform.position, Quaternion.identity, gameFSMObject.transform);

            // Spawn Camera
            GameObject.Instantiate(Resources.Load("Prefabs/Game/MainCamera"), gameFSMObject.transform.position, Quaternion.identity, gameFSMObject.transform);
            Game.instance.loadedGameManagers = true;
        }

        if (PlayerManager.instance.player != null)
        {
            PlayerManager.instance.player.SetActive(true);
        }

        yield break;
    }

    public override IEnumerator Exit()
    {
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
