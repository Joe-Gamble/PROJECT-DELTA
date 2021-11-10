using UnityEngine;

namespace Weapons
{
    namespace Guns
    {
        public abstract class Automatic : Gun
        {
            private void Start() {
            
                m_GunType = GunTypes.AUTOMATIC;
            }
        }

        public abstract class SingleShot : Gun
        {
            private void Start() {
            
                m_GunType = GunTypes.SINGLESHOT;
            }
        }

        public abstract class Hybrid : Gun
        {
            private void Start() {
                
                m_GunType = GunTypes.HYBRID;
            }
        }
    }
}

public abstract class Weapon : InteractableItem, IWeaponInspectable
{
    public abstract WeaponTypes WeaponType {get;}

    public void Inspect()
    {
        //play animation here
    }
}

public abstract class Gun : Weapon, IShootable
{
    public override WeaponTypes WeaponType => WeaponTypes.GUN;
    public override ItemData data => gun_data as ItemData;

    public abstract GunData gun_data {get; }

    protected GunTypes m_GunType {get; set;}

    protected int ammo_in_clip;
    protected int ammo_in_reserves;

    private void Start() {
        //ammo_in_clip = data.clip_size;
        //ammo_in_reserves = data.reserve_mags * ammo_in_clip;
    }

    public override void Use()
    {
        Shoot();
    }

    public virtual void Shoot(){ throw new System.NotImplementedException(); }
}

public abstract class Melee : Weapon, ISwingable
{
    public override WeaponTypes WeaponType => WeaponTypes.MELEE;

    private void Start() {
       
    }

    public override void Use()
    {
        Swing();
    }

    public virtual void Swing() { throw new System.NotImplementedException(); }
}

