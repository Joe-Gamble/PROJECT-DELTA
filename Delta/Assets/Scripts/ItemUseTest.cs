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
        Spawner.Spawn(spawnPos, weap, false, Spawner.SpawnOptions.DYNAMIC_NO_PLAYER_COLLISION);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
