using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pillar : Subject
{
    private EnergeticField field;
    private int maxHP = 40;
    public Image HPBar;

    void Awake()
    {
        SetHP();
        field = transform.parent.GetComponent<EnergeticField>();
    }

    public override void applyDamage(int damage)
    {
        base.applyDamage(damage);

        if (HPBar)
        {
            HPBar.fillAmount = HP * 1.0f / maxHP * 1.0f;
        }
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
