using UnityEngine;

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

    [Header("Sound Files")]
    public AudioClip reload_sfx;
    public AudioClip fire_sfx;

    [Header("Weapon Gfx")]
    public int placeholder;
}