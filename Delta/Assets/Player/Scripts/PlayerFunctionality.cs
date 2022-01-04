using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFunctionality : MonoBehaviour
{
    EquipmentHandler equipmentHandler = new EquipmentHandler();
    InputManager inputManager;
    ObjectLook objectLook;

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
                PhysicalItem item = objectLook.getFocus().GetComponent<ItemRef>().GetData();
                item.Collect();

                if (item is IItemUseable)
                {
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
