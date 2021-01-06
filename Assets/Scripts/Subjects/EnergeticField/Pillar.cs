using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pillar : Subject
{
    private EnergeticField field;
    private int maxHP = 100;

    void Awake()
    {
        SetHP();
        field = transform.parent.GetComponent<EnergeticField>();
    }

    protected override void Die()
    {
        field.pillarDestroyed();
        gameObject.SetActive(false);
    }

    public void SetHP()
    {
        HP = maxHP;
    }
}
