using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [Header("FSM")]
    private GameFSM stateMachine;

    void Start()
    {
        stateMachine = GetComponent<GameFSM>();
        stateMachine.changeState(new StartState());
        stateMachine.changeState(new PauseState());
    }
}
