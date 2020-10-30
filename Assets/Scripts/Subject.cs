using UnityEngine;

abstract public class Subject : MonoBehaviour
{
    public int HP { get; protected set; }
    public int Shield { get; protected set; }
    public float Speed { get; protected set; }

    // !!!!MAKE ME ABSTRACT!!!!
    public void applyDamage(int damage)
    {
        // Play sound and annimation

        // Substract hp and check if player should die
        HP -= damage;
        if (HP <= 0)
            Die();
    }

    protected abstract void Die();
}
