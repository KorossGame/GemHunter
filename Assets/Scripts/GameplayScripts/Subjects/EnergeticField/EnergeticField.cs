using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnergeticField : Subject
{
    private NavMeshAgent bossObjectAI;
    [SerializeField] private GameObject sphereVisual;

    private float rechargeTime = 15f;
    private int maxHP = 4;

    void Awake()
    {
        SetHP();
        bossObjectAI = GameObject.FindGameObjectWithTag("Boss").GetComponent<NavMeshAgent>();
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

    public override void Die()
    {
        sphereVisual.SetActive(false);
        StartCoroutine(Recharge());
    }

    private IEnumerator Recharge()
    {
        // Disactivate energetic field
        gameObject.GetComponent<SphereCollider>().enabled = false;
        bossObjectAI.baseOffset = 0.5f;

        // Wait for 10 seconds
        yield return new WaitForSeconds(rechargeTime);

        // As for recharge time boss can be killed we need to check if need to activate field
        if (!bossObjectAI) Destroy(gameObject);

        // Activate the energetic field
        gameObject.GetComponent<SphereCollider>().enabled = true;
        bossObjectAI.baseOffset = 0.75f;

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
