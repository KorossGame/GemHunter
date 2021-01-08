using UnityEngine;

abstract public class Subject : MonoBehaviour
{
    public int HP { get; protected set; }
    public byte Shield { get; protected set; }
    public float Speed { get; set; }

    public virtual void applyDamage(int damage)
    {
        AudioManager.instance.PlaySound("Hit");

        // Substract hp and check if object should die
        HP -= damage;
        if (HP <= 0)
            Die();
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }
}
