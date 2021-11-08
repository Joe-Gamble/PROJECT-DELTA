using UnityEngine;

#region Interfaces
     
public interface IItemUseable 
{
    void Use();
    void UseSecond();
}

public interface IItemCollectable
{
    void Collect();
}

public interface IItemDropable
{
    void Drop();
}


#region Weapon Interfaces

public interface IWeaponInspectable
{
    void Inspect();
}

public interface IWeaponAimable
{
    void Aim();
}

//GUN INTERFACES
public interface IReloadable
{
    void Reload();
}

public interface IShootable
{
    void Shoot();
}

public interface IUpgradeable
{
    void Upgrade();
}

//MELEE INTERFACES
public interface ISwingable
{
    void Swing();
}

#endregion

#endregion

public class Item : MonoBehaviour, IItemDropable, IItemCollectable
{
    public GameObject model;
    public ScriptableObject data;

    private Rigidbody rb;

    private void Start() {
        rb = model.AddComponent<Rigidbody>();
    }

    public virtual void Drop()
    {

    }

    public virtual void Collect()
    {

    }
}

public abstract class Weapon : Item, IWeaponInspectable, IItemUseable
{
    protected WeaponTypes m_WeaponType {get; set;}

    public abstract void Use();

    public abstract void UseSecond();

    public void Inspect()
    {
        //play animation here
    }
}

public abstract class Gun : Weapon, IShootable
{
    protected GunTypes m_GunType {get; set;}
    protected GunData m_GD {get; set;}

    protected int current_ammo;

    private void Start() {
        m_WeaponType = WeaponTypes.GUN;
        current_ammo = m_GD.clip_size;
    }

    public override void Use()
    {
        Shoot();
    }

    public abstract void Shoot();
}

public abstract class Automatic : Gun
{
    private void Start() {
        
        m_GunType = GunTypes.AUTOMATIC;
    }

    public override void Shoot(){
        //Shoot Code
    }

    private void Update() {
        
    }
}