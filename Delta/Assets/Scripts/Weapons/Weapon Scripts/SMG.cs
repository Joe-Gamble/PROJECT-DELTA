using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//throw new System.NotImplementedException();


public class SMG : Automatic, IReloadable, IUpgradeable, IWeaponAimable
{
    private void Start() {

        m_GD = (GunData)data;
    }

    public override void UseSecond()
    {
        Aim();
    }

    public void Aim()
    {
        //Aim Code here
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