using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ItemDetails
{
    public ItemDetails(GameObject go, SpawnStates state, PhysicalItem data)
    {
        obj_ref = go;
        spawn_state = state;
        item = data;
    }
    public GameObject obj_ref;
    public SpawnStates spawn_state;
    public PhysicalItem item;
}

//Store "Deleted Objects" in a disabled list of structs, containing  a reference to the itemdata, spawnoptions
public enum SpawnStates
{
    STATIC_NO_PLAYER_COLLISION,
    STATIC_PLAYER_COLLISION,
    DYNAMIC_NO_PLAYER_COLLISION,
    DYNAMIC_PLAYER_COLLISION
}
