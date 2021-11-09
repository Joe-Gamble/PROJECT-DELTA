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

public class Item : MonoBehaviour
{
    public ItemData data;
}

public class PhyscialItem : Item, IItemDropable, IItemCollectable
{
    public GameObject model;

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

public abstract class InteractableItem : PhyscialItem, IItemUseable
{
    public abstract void Use();

    public abstract void UseSecond();
}