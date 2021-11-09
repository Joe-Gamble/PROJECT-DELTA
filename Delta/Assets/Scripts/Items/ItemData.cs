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

public class ItemData : ScriptableObject 
{
    [Header("Item Details")]
    public int id = 0;
    public string item_name = "";
    public string desc = "";

    public Sprite item_preview;

    [Header("Item GFX")]
    public Mesh model;
}

public abstract class WeaponData : ItemData 
{
    [Header("Weapon Values")]
    public int damage;
    public float rateOfFire;
}
