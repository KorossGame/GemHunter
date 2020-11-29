using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFSM : FSM
{
    public new BossState currentState;

    public void changeState(BossState newState)
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
