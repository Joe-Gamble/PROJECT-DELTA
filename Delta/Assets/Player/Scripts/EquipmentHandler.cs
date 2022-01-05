using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Can we have this as a Static/Singleton? 
public class EquipmentHandler
{
    //Does this need to default to hands?

    //Interactable list for multiple weapons & Single GameObject to store current?
    InteractableItem equiped_item = null;



    public InteractableItem GetEquiped()
    {
        return equiped_item;
    }

    public void EquipItem(Transform root, InteractableItem item_data)
    {
        if (equiped_item != null)
        {
            UnequipItem();
        }

        GameObject new_item = item_data.runtime_ref;

        equiped_item = item_data as InteractableItem;

        new_item.transform.parent = root.transform;
        new_item.transform.position = root.transform.position;
        new_item.transform.rotation = root.transform.rotation;

        new_item.layer = 7;

        SetLayerRecursively(new_item, 6);

        new_item.transform.Rotate(item_data.data.equip_rot_offset);

        if (new_item.TryGetComponent<Rigidbody>(out Rigidbody rb))
        {
            rb.isKinematic = true;
        }
    }

    public void UnequipItem()
    {
        //Drop here
        SetLayerRecursively(equiped_item.runtime_ref, 7);

        //This might not be ok - how can we be sure that the item wasn't kinetamtic to begin with? 
        //How do we deal with static items that we collect?

        if (equiped_item.runtime_ref.TryGetComponent<Rigidbody>(out Rigidbody rb))
        {
            rb.isKinematic = false;
        }

        equiped_item.Drop();
        equiped_item = null;
    }

    void SetLayerRecursively(GameObject obj, int new_layer)
    {
        obj.layer = new_layer;

        foreach (Transform child in obj.transform)
        {
            SetLayerRecursively(child.gameObject, new_layer);
        }
    }


}
