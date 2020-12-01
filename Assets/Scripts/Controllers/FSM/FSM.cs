using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FSM : MonoBehaviour
{
    public State currentState;

    public void changeState(State newState)
    {
        if (currentState != null)
        {
            // Exit from previous state
            StartCoroutine(currentState.Exit());
        }

        // Set current state to a new one
        currentState = newState;

        // Initialize new phase
        StartCoroutine(currentState.Enter());
    }
}
