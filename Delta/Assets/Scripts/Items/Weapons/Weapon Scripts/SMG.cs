using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//throw new System.NotImplementedException();


public class SMG : Automatic, IReloadable, IUpgradeable, IWeaponAimable
{
    private void Start() {
        
    }

    public override void UseSecond()
    {
        Aim();
    }

    public override void Shoot()
    {
        Debug.Log("Shooting");
    }

    public void Aim()
    {
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
}