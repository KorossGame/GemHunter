﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseState : GameState
{
    public PauseState() : base()
    {

    }

    public override IEnumerator Enter()
    {
        Time.timeScale = 0;
        yield break;
    }

    public override IEnumerator Exit()
    {
        Time.timeScale = 1;
        yield break;
    }
}