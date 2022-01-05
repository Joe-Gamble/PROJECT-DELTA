using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFunctionality : MonoBehaviour
{
    EquipmentHandler equipmentHandler = new EquipmentHandler();
    InputManager inputManager;
    ObjectLook objectLook;

    public GameObject handler_root;

    // Start is called before the first frame update
    void Start()
    {
        inputManager = InputManager.Instance;
        objectLook = GetComponent<ObjectLook>();
    }

    // Update is called once per frame
    void Update()
    {
        if (inputManager.Interact())
        {
            if (objectLook.hasFocus())
            {
                GameObject target = objectLook.getFocus();

                PhysicalItem item = target.GetComponent<ItemRef>().GetData();

                item.Collect();

                if (item is IItemUseable)
                {
                    equipmentHandler.EquipItem(target, handler_root.transform, item as InteractableItem);
                    Debug.Log("Item Can be Used");
                }
            }
        }

        if (equipmentHandler.GetEquiped() != null)
        {
            if (inputManager.OnLeftClickDown())
            {
                equipmentHandler.GetEquiped().Use();
            }

            if (inputManager.OnRightClickDown())
            {
                equipmentHandler.GetEquiped().UseSecond();
            }
        }
    }
}
