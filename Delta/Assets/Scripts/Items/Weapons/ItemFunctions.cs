using UnityEngine;

#region Interfaces

public interface IItemUseable
{
    void Use();
    void OnUseSecond();
    void PrimaryStop();
    void SecondaryStop();
}

public interface IItemCancelable
{
    void Cancel();
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

public abstract class Item
{
    public abstract string data_path { get; }
    public abstract ItemData data { get; }
}

public abstract class PhysicalItem : Item, IItemDropable, IItemCollectable
{
    public GameObject runtime_ref = null;

    public virtual void Drop()
    {

    }

    //Difference between manual collection and automatic by collision?
    public virtual void Collect()
    {

    }
}

public abstract class InteractableItem : PhysicalItem, IItemUseable
{
    public abstract void Use();

    public abstract void OnUseSecond();

    public abstract void PrimaryStop();

    public abstract void SecondaryStop();
}




