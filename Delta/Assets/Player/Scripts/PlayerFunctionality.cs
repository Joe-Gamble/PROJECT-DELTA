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
                ItemRef item = target.GetComponent<ItemRef>();

                if (item.GetData() is IItemUseable)
                {
                    Debug.Log("Item is usable");
                    equipmentHandler.EquipItem(handler_root.transform, item);
                }
            }
        }

        if (equipmentHandler.GetEquiped() != null)
        {
            if (inputManager.OnLeftClick())
            {
                equipmentHandler.GetEquiped().Use();
            }

            if (inputManager.OnLeftClickUp())
            {
                equipmentHandler.GetEquiped().PrimaryStop();
            }

            if (inputManager.OnRightClick())
            {
                equipmentHandler.GetEquiped().UseSecond();
            }

            if (inputManager.OnRightClickUp())
            {
                equipmentHandler.GetEquiped().SecondaryStop();
            }
        }
    }
}
