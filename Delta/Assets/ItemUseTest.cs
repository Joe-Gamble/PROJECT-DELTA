using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Reflection;

public class ItemUseTest : MonoBehaviour
{
    public GameObject g_Item;
    public EquipmentHandler equipmentHandler = new EquipmentHandler();

    public bool toggle;

    InputManager inputManager;
    // Start is called before the first frame update
    void Start()
    {
        inputManager = InputManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if(inputManager != null)
        {
            if(inputManager.Interact())
            {
                SMG weap = Factory.Manager.GetItem<SMG>("SMG");

                Type[] types = {typeof(Gun)}; 
                Type[] interfaces = {typeof(IReloadable), typeof(IShootable)};

                foreach(Weapon name in Factory.Manager.GetCollection<Weapon>(types,interfaces))
                {
                    Debug.Log(name.GetType().Name);
                }
            }
            if(equipmentHandler.GetEquiped() != null)
            {
                if(inputManager.OnLeftClickDown())
                {
                    equipmentHandler.GetEquiped().Use();
                    //Debug.Log("Left Click");
                }

                if(inputManager.OnRightClickDown())
                {
                    equipmentHandler.GetEquiped().UseSecond();
                    Debug.Log("Right Click Down");
                }

                if(inputManager.OnRightClickUp())
                {
                    //weapon.UseSecond();
                    Debug.Log("Right Click Up");
                }
            }
        }
    }
}