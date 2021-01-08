using System.Collections;

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
