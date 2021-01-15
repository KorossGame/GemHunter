using UnityEngine;

abstract public class Subject : MonoBehaviour
{
    public int HP { get; protected set; }
    public byte Shield { get; protected set; }
    public float Speed { get; set; }

    public bool activated = true;

    protected Animator animator;
    protected string currentState;

    public virtual void applyDamage(int damage)
    {
        AudioManager.instance.PlaySound("Hit");

        if (animator != null)
        {
            ChangeAnimationState("DamageAnimation");
        }

        // Substract hp and check if object should die
        HP -= damage;
        if (HP <= 0)
            Die();
    }

    protected virtual void Die()
    {
        DeleteObject();
    }

    protected void DeleteObject()
    {
        Destroy(gameObject);
    }

    public void ChangeAnimationState(string newState)
    {
        if (currentState == "Die") return;
        if (currentState == newState) return;
        animator.Play(newState);
        currentState = newState;
    }

    protected virtual void GoToMenu()
    {
        //
    }
}
