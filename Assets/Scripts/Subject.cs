using UnityEngine;

abstract public class Subject : MonoBehaviour
{
    public int HP { get; set; }
    public int Shield { get; set; }
    public float Speed { get; set; }
    public Gun WeaponEquiped { get; set; }
}
