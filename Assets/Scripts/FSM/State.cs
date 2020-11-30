using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    public virtual IEnumerator Enter()
    {
        yield break;
    }

    public virtual IEnumerator Exit()
    {
        yield break;
    }
}
