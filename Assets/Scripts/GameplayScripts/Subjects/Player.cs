using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : Subject
{
    private float maxHP;

    // UI
    [SerializeField] private Image hpValue;
    public GameObject damagePowerUP;
    public GameObject speedPowerUP;
    public GameObject godPowerUP;

    public GameObject visualObject;

    // Respawn point
    public Transform spawnPoint;

    // Time till respawn the player
    private float respawnTime = 5f;

    // Inventory system
    public WeaponSwitcher inventory;

    /* Multipliers better to make as listeners and throw an event when player picks up a powerup (Security reasons) */

    // Damage multiplier value for guns (for power up mechanics)
    public byte GunPowerUPMultiplier { get; set; } = 1;
    public float SpeedBonusMultiplier { get; set; } = 1;

    // If player can be damaged (for power up mechanics)
    public bool GodMode { get; set; } = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        GodMode = Game.instance.GodModeActivated;
        Speed = 10f;
        HP = 250;
        maxHP = HP;
        hpValue.fillAmount = HP / maxHP;
        Spawn();
    }

    public override void applyDamage(int damage)
    {
        if (!GodMode)
        {
            // Calc damage
            base.applyDamage(damage);
            hpValue.fillAmount = HP / maxHP;
        }
    }

    protected override void Die()
    {
        AudioManager.instance.PlaySound("DieSound");
        if (animator != null)
        {
            ChangeAnimationState("Die");
        }
    }

    protected override void GoToMenu()
    {
        // Used by animations
        Game.instance.stateMachine.changeState(new MenuState(Game.instance.stateMachine));
    }

    private void Spawn()
    {
        // Teleport player to the spawn point
        if (spawnPoint)
        {
            gameObject.transform.position = spawnPoint.position;
        }
        else
        {
            gameObject.transform.position = new Vector3(0, 0.5f, 0);
        }

        // Enable Visual player object
        visualObject.GetComponent<MeshRenderer>().enabled = true;
    }
}