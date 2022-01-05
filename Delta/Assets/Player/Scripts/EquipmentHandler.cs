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

    public void EquipItem(GameObject new_item, Transform root, InteractableItem item_data)
    {
        //Will eventually come from inventory but for now will appear from void
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
