using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region Enums

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

#endregion

public class ItemData : ScriptableObject
{
    [Header("Dev Details")]
    public int id = 0;

    [Header("Game Details")]
    public string item_name = "";
    public string desc = "";

    public ItemTypes type = ItemTypes.UNDEFINED;

    public Sprite item_preview;

    [Header("Item GFX")]
    public GameObject prefab;
    public Vector3 col_size;
    public Vector3 equip_rot_offset;
}

public abstract class WeaponData : ItemData
{
    [Header("Weapon Values")]
    public int damage;

}

/*
    -- Ammo --
    Arrows, Bullets, Grenades
    -Should Spawn a Prefab to allow for monobehaviour functionality on ammo objects
    -Ammo objects should use a universal damage collider
    -Standardised hitscan speeds
    -Projectile speeds differ
    -Damage should come from weapon data
    -Contains gfx for 
            -bullet trail
            -muzzle flash / shooting gfx (if any)
            -impact visuals
            -Explosion visuals? (Optional)

    -Needs functions for
            -Spawning
            -Impacts
            -Explosion (Optional Iterface)
*/
