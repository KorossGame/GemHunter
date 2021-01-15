using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    public static Game instance;

    [Header("FSM")]
    public GameFSM stateMachine;

    public bool loadedGameManagers { get; set; } = false;
    public int latestGameScene;

    public bool GodModeActivated { get; set; } = false;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        stateMachine = GetComponent<GameFSM>();
        stateMachine.changeState(new PreMenuState(stateMachine));
    }

    private void Update()
    {
        StartCoroutine(stateMachine.currentGameState.Play());
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != "LoadingScreen")
        {
            // Save new game scene to load new level futher
            latestGameScene = scene.buildIndex;
            
            // After loading screen we need to set player active
            if (scene.name != "Menu")
            {
                if (PlayerManager.instance && PlayerManager.instance.player && PlayerManager.instance.gameObject.activeSelf)
                {
                    PlayerManager.instance.player.SetActive(true);
                }
            }
        }

        /* Handle audio */

        AudioManager.instance.StopSound("TutorialMusic");
        AudioManager.instance.StopSound("LevelMusic");
        AudioManager.instance.StopSound("BossMusic");

        if (scene.name == "Tutorial")
        {
            AudioManager.instance.PlaySound("TutorialMusic");
        }
        else if (scene.name == "Level")
        {
            AudioManager.instance.PlaySound("LevelMusic");
            Spawner.instance.active = true;
        }
        else if (scene.name == "Boss")
        {
            AudioManager.instance.PlaySound("BossMusic");
        }
    }
}
