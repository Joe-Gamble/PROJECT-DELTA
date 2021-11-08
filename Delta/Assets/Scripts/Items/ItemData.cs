using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemTypes
{
    UNDEFINED = -1,
    WEAPON
    //MORE TO BE ADDED
}

public enum WeaponTypes
{
    UNDEFINED = -1,
    GUN,
    MELEE,
    SPECIAL,
}

public enum GunTypes
{
    UNDEFINED = -1,
    SINGLESHOT,
    AUTOMATIC,
    BURST,
    HYBRID,
}

public abstract class ItemData : ScriptableObject 
{
    [Header("Item Details")]
    public int id = 0;
    public string item_name = "";
    public string desc = "";

    public Sprite item_preview;
}

public abstract class WeaponData : ItemData 
{
    [Header("Weapon Values")]
    public int damage;
    public float rateOfFire;
}


[CreateAssetMenu(fileName = "Data", menuName = "Items/Weapons/Gun", order = 0)]
public class GunData : WeaponData
{
    [Header("Gun Values")]
    public int fire_rate = 0;
    public float reload_speed = 0;
    public int clip_size = 0;
    public int reserve_mags = 0;

    [Header("Gun Behaviours")]
    public AnimationCurve start_up_rate = null;
    public float recoil = 0.0f;
    public float spread = 0.0f;
}
