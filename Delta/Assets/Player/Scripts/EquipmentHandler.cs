using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentHandler
{
    //Does this need to default to hands?
    
    InteractableItem equiped_item = null;

    public InteractableItem GetEquiped()
    {
        return equiped_item;
    }

    public void EquipItem(InteractableItem new_item)
    {
        //Will eventually come from inventory but for now will appear from void
        equiped_item = new_item;
    }

    public void UnequipItem()
    {
        equiped_item = null;
    }
}
