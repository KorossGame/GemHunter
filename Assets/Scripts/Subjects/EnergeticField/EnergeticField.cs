using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergeticField : Subject
{
    private float rechargeTime = 10f;
    private int maxHP = 4;

    void Awake()
    {
        SetHP();
    }

    void SetHP()
    {
        HP = maxHP;
    }

    public override void applyDamage(int damage)
    {
        // THERE IS NO DAMAGE FROM GUNS
    }

    public void pillarDestroyed()
    {
        HP--;
        if (HP <= 0)
            Die();
    }

    protected override void Die()
    {
        StartCoroutine(Recharge());
    }

    private IEnumerator Recharge()
    {
        // Disactivate energetic field
        gameObject.GetComponent<SphereCollider>().enabled = false;

        // Wait for 10 seconds
        yield return new WaitForSeconds(rechargeTime);

        // Activate the energetic field
        gameObject.GetComponent<SphereCollider>().enabled = true;

        // Regen the HP
        SetHP();

        // Activate all the pillars
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
            child.gameObject.GetComponent<Pillar>().SetHP();
        }
    }
}
