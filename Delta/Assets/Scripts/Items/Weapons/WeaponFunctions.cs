using UnityEngine;

namespace Weapons
{
    namespace Guns
    {
        public abstract class Automatic : Gun
        {
            public override GunTypes m_GunType => GunTypes.AUTOMATIC;

        }

        public abstract class SingleShot : Gun
        {
            public override GunTypes m_GunType => GunTypes.SINGLESHOT;
        }

        public abstract class Hybrid : Gun
        {
            public override GunTypes m_GunType => GunTypes.HYBRID;
        }
    }
}

public abstract class Weapon : InteractableItem, IWeaponInspectable
{
    public abstract WeaponTypes WeaponType { get; }

    public void Inspect()
    {
        //play animation here
    }
}

public abstract class Gun : Weapon, IShootable
{
    public override WeaponTypes WeaponType => WeaponTypes.GUN;
    public override ItemData data => gun_data as ItemData;

    public abstract GunData gun_data { get; }
    public virtual GunTypes m_GunType { get; set; }

    protected int ammo_in_clip;
    protected int ammo_in_reserves;

    private float current_time = 0;
    private float time_since_last;


    public override void Use()
    {
        Shoot();
    }

    public void Shoot()
    {
        time_since_last = Time.time - current_time;
        if (time_since_last >= 1f / (gun_data.fire_rate / 60.0f))
        {
            Debug.Log("Fire");
            time_since_last = 0;
            current_time = Time.time;
        }
    }

    public override void PrimaryStop()
    {

    }

    public override void SecondaryStop()
    {
        throw new System.NotImplementedException();
    }
}

public abstract class Melee : Weapon, ISwingable
{
    public override WeaponTypes WeaponType => WeaponTypes.MELEE;

    private void Start()
    {

    }

    public override void Use()
    {
        Swing();
    }

    public virtual void Swing() { throw new System.NotImplementedException(); }
}

