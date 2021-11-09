using UnityEngine;

public abstract class Weapon : InteractableItem, IWeaponInspectable
{
    protected WeaponTypes m_WeaponType {get; set;}

    public void Inspect()
    {
        //play animation here
    }
}

public abstract class Gun : Weapon, IShootable
{
    protected GunTypes m_GunType {get; set;}
    protected GunData m_GD {get; set;}

    protected int ammo_in_clip;
    protected int ammo_in_reserves;

    private void Start() {
        m_GD = (GunData)data;
        m_WeaponType = WeaponTypes.GUN;
        ammo_in_clip = m_GD.clip_size;
        ammo_in_reserves = m_GD.reserve_mags * ammo_in_clip;
    }

    public virtual void Shoot(){}
}

public abstract class Automatic : Gun
{
    private void Start() {
        
        m_GunType = GunTypes.AUTOMATIC;
    }

    public override void Use()
    {
        Shoot();
    }

    public override void Shoot()
    {
        
    }

    private void Update() {
        
    }
}