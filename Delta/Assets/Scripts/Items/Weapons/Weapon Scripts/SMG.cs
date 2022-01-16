using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Weapons.Guns;

public class SMG : Automatic, IReloadable, IUpgradeable, IWeaponAimable
{
    public override string data_path => "Data/TEST SMG";
    public override Data<GunData> data => new Data<GunData>(Resources.Load<GunData>(data_path));

    protected override ItemData GetData() { return data.GetData(); }
    public new GunData Data() { return this.GetData() as GunData; }

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