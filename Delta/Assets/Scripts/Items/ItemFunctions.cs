using UnityEngine;

#region Item Interfaces

public interface IData<T>
{
    T GetData();
}

public interface ISerializable
{
    void SetData<T>(T data);
}

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

public class Data<T> : IData<T>
{
    public Data(T m_data)
    {
        data = m_data;
    }
    public virtual T GetData() { return data; }
    private T data { get; }
}

public abstract class Item
{
    protected Data<ItemData> data;
    public abstract string data_path { get; }

    protected virtual ItemData GetData() { return data.GetData(); }
    public ItemData Data() { return this.GetData(); }
}

public abstract class PhysicalItem : Item
{
    protected new Data<InstanceData> data;
    public GameObject runtime_ref = null;

    protected override ItemData GetData() { return data.GetData(); }
    public new InstanceData Data() { return this.GetData() as InstanceData; }
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




