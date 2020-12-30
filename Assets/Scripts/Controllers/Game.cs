using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public static Game instance;

    [Header("FSM")]
    public GameFSM stateMachine;

    void Awake()
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
        stateMachine.changeState(new MenuState(stateMachine));
    }
}
