using UnityEngine;

abstract public class Gun : MonoBehaviour
{
    protected int weaponID;
    public int DamagePerShot { get; set; }
    public int CurrentAmmo { get; set; }
    protected int ammoInClip;
    protected int maxAmmo;
    protected float effectiveRange;
    protected float maxRange = 100;
    protected float reloadTime;
    protected float cooldownTime;

    protected GameObject bulletPrefab;

    public void Shoot(GameObject player)
    {
        RaycastHit hit;

        if (Physics.Raycast(player.transform.position, player.transform.forward, out hit, maxRange))
        {
            Subject target = hit.transform.GetComponent<Subject>();
            target.applyDamage(DamagePerShot);
        }
        
        // Instanciate bullet prefab
        // Calc RayCast
    }
    public void Reload()
    {
        // Reload logic
    }

    public void SwitchWeapon(int currentWeapon, int newCurrentWeapon)
    {
         /*
          * if player picked new weapon, it becomes enabled
          * if player wants to change weapons by mouse wheel - switch to previous/next enabled weapon
          * if player wants to change weapon by numbers - change to specific weapon if enabled
          */
          
    }
}
