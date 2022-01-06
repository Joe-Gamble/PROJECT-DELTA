using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//throw new System.NotImplementedException();

using Weapons.Guns;

public class SMG : Automatic, IReloadable, IUpgradeable, IWeaponAimable
{
    public override string data_path => "Data/TEST SMG";
    public override GunData gun_data => Resources.Load<GunData>(data_path) as GunData;

    //ammo_in_clip = data.clip_size;
    //ammo_in_reserves = data.reserve_mags * ammo_in_clip;


    public override void UseSecond()
    {
        Aim();
    }

    public void Aim()
    {
        //Cinemachine Blend to ADS Cam
        //Aim Code here
        Debug.Log("Aiming");
    }

    public void Reload()
    {
        //Reload Code here
    }

    public void Upgrade()
    {
        //Upgrade functionality - later addition?
    }

    private void Update()
    {
        Debug.Log("Update");
    }
}