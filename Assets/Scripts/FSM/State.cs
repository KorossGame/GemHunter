using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : MonoBehaviour
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
