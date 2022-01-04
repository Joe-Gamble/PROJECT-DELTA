using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Reflection;

public class ItemUseTest : MonoBehaviour
{
    public EquipmentHandler equipmentHandler = new EquipmentHandler();
    public GameObject spawnPrefab;
    public Transform spawnPos;

    InputManager inputManager;
    // Start is called before the first frame update
    void Start()
    {
        inputManager = InputManager.Instance;

        SMG weap = Factory.GetItem<SMG>("SMG");
        Spawner.Spawn(spawnPos, weap, false, false);
    }

    // Update is called once per frame
    void Update()
    {
        if (inputManager != null)
        {
            if (inputManager.Interact())
            {
                //GET GENERIC TYPE FROM NAME
                InteractableItem my_gun = Factory.GetItem<SMG>("SMG");

                equipmentHandler.EquipItem(my_gun);

                if (my_gun is IReloadable)
                {
                    //reload the gun
                }
            }
        }
    }
}
