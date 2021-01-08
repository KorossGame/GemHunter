using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFSM : FSM
{
    public List<BossState> possibleStates;

    public BossState currentBossState;

    public void Start()
    {
        possibleStates = new List<BossState>();
    }

    public void AddState(BossState gameState)
    {
        possibleStates.Add(gameState);
    }

    public void changeState(BossState newState)
    {
        if (currentBossState != null)
        {
            // Exit from previous state
            StartCoroutine(currentBossState.Exit());
        }

        // Set current state to a new one
        currentBossState = newState;

        // Initialize new phase
        StartCoroutine(currentBossState.Enter());
    }
}
