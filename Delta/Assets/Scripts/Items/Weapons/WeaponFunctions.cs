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

public abstract class Weapon : InteractableItem, IWeaponInspectable, IItemCollectable
{
    public abstract WeaponTypes WeaponType { get; }

    public void Inspect()
    {
        //play animation here
    }

    public virtual void Drop()
    {

    }

    //Difference between manual collection and automatic by collision?
    public virtual void Collect(GameObject player)
    {

    }
}

public abstract class Gun : Weapon, IShootable
{
    public override WeaponTypes WeaponType => WeaponTypes.GUN;

    public abstract GunData data { get; }
    public virtual GunTypes m_GunType { get; set; }

    protected int ammo_in_clip;
    protected int ammo_in_reserves;

    private float current_time = 0;
    private float time_since_last;

    public override InstanceData GetData()
    {
        return data;
    }


    public override void Use()
    {
        time_since_last = Time.time - current_time;
        if (time_since_last >= 1f / (data.fire_rate / 60.0f))
        {
            Shoot();
            time_since_last = 0;
            current_time = Time.time;
        }
    }

    public void Shoot()
    {
        Debug.Log("Fire");
    }

    public override void PrimaryStop()
    {

    }

    public override void UseSecond()
    {
        throw new System.NotImplementedException();
    }

    public override void SecondaryStop()
    {
        throw new System.NotImplementedException();
    }
}

public abstract class Melee : Weapon, ISwingable
{
    public override WeaponTypes WeaponType => WeaponTypes.MELEE;

    public override void Use()
    {
        /*  
        time_since_last = Time.time - current_time;
        if (time_since_last >= 1f / (gun_data.fire_rate / 60.0f))
        {
            Swing();
            time_since_last = 0;
            current_time = Time.time;
        }
        */
    }

    //Do we need to move this functionality deeper to use the shoot code?
    public virtual void Swing() { throw new System.NotImplementedException(); }
}

