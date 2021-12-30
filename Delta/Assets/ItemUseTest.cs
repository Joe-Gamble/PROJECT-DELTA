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
    }

    // Update is called once per frame
    void Update()
    {
        if(inputManager != null)
        {
            if(inputManager.Interact())
            {
                //GET SPECIFIC TYPE FROM NAME
                SMG weap = Factory.Manager.GetItem<SMG>("SMG");

                SpawnItem(spawnPos, weap);

                
                //GET SPECIFIC TYPE FROM NAME
                SMG smg = Factory.Manager.GetItem<SMG>("SMG");

                //FIND ALL TYPES OF A TYPE
                List<Gun> AllGuns = Factory.Manager.GetAllItemsOfType<Gun>();

                //FIND TYPES WITH SPECIFIC BEHAVIOUS
                Type[] types = {typeof(Gun), (typeof(Melee))}; 
                Type[] interfaces = {typeof(IReloadable), typeof(IShootable), typeof(ISwingable)};

                //USING DEFINED COLLECTIONS FOR TYPES AND BEHAVIOURS; RETURNS A LIST OF ALL TYPES FOUND
                List<Weapon> FoundWeapons = Factory.Manager.GetSpecificCollection<Weapon>(types,interfaces);

                //GET GENERIC TYPE FROM NAME
                InteractableItem my_gun = Factory.Manager.GetItem<SMG>("SMG");

                if(my_gun is IReloadable)
                {
                    //reload the gun
                }


            }
            if(equipmentHandler.GetEquiped() != null)
            {
                if(inputManager.OnLeftClickDown())
                {
                    equipmentHandler.GetEquiped().Use();
                }

                if(inputManager.OnRightClickDown())
                {
                    equipmentHandler.GetEquiped().UseSecond();
                }
            }
        }
    }

    void SpawnItem(Transform pos, PhysicalItem item)
    {
        if(item.data.type != ItemTypes.UNDEFINED)
        {
            GameObject obj = GameObject.Instantiate(spawnPrefab, pos.position, pos.rotation);

            ItemRef item_ref = obj.GetComponent<ItemRef>();
            item_ref.Spawn(obj, item);
        }
       
    }
}
