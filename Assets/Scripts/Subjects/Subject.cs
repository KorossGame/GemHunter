using UnityEngine;

abstract public class Subject : MonoBehaviour
{
    public int HP { get; protected set; }
    public int Shield { get; protected set; }
    public float Speed { get; set; }

    // !!!!MAKE ME ABSTRACT!!!!
    public virtual void applyDamage(int damage)
    {
        // Substract hp and check if player should die
        HP -= damage;
        if (HP <= 0)
            Die();
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }
}
