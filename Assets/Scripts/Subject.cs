using UnityEngine;

abstract public class Subject : MonoBehaviour
{
    public int HP { get; set; }
    public int Shield { get; set; }
    public float Speed { get; set; }
    public Gun WeaponEquiped { get; set; }
    public void applyDamage(int damage)
    {
        // Play sound and annimation

        // Substract hp and check if player should die
        HP -= damage;
        Debug.Log(HP);
        if (HP <= 0)
            Die();
    }

    protected void Die()
    {
        // Play animation

        // Play sound

        Destroy(gameObject);
    }
}
