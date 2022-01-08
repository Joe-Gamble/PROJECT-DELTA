using UnityEngine;

#region Item Interfaces

public interface IItemUseable
{
    void Use();
    void UseSecond();
    void PrimaryStop();
    void SecondaryStop();
}

public interface IItemCancelable
{
    void Cancel();
}

public interface IItemCollectable
{
    void Collect(GameObject player);
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

public abstract class Item<T> : ItemData
{
    public abstract string data_path { get; }
    public abstract T GetData();
}

public abstract class PhysicalItem : Item<InstanceData>
{
    public GameObject runtime_ref = null;
}

public abstract class InteractableItem : PhysicalItem, IItemUseable
{
    public abstract void Use();

    public abstract void UseSecond();

    public abstract void PrimaryStop();

    public abstract void SecondaryStop();
}





public abstract class CollectableItem : PhysicalItem, IItemDropable, IItemCollectable
{
    public abstract void Drop();

    //Difference between manual collection and automatic by collision?
    public abstract void Collect(GameObject player);
}




