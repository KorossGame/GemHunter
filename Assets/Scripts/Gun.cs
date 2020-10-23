abstract public class Gun
{
    public int HpDamage { get; set; }
    public int ShildDamage { get; set; }

    protected string name;
    protected int range;
    protected byte maxAmmo;
    protected float fireInterval;
    protected int weight;
}
