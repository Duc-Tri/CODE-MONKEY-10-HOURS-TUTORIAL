using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlatesCounter : BaseCounter
{
    public event EventHandler OnPlateSpawned;
    public event EventHandler OnPlateRemoved;

    [SerializeField] private KitchenObjectsSO plateKitchenObjectSO;

    private float spawnPlateTimer;
    private float spawnPlateTimerMax = 2;
    private int platesSpawnAmount;
    private int platesSpawnAmountMax = 5;

    private void Update()
    {
        spawnPlateTimer += Time.deltaTime;
        if (spawnPlateTimer > spawnPlateTimerMax)
        {
            //KitchenObject.SpawnKitchenObject(plateKitchenObjectSO, this);
            spawnPlateTimer = 0;
            if (platesSpawnAmount < platesSpawnAmountMax)
            {
                platesSpawnAmount++;

                OnPlateSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
            // player is empty handed
            if (platesSpawnAmount > 0)
            {
                {
                    // at least one plate here
                    platesSpawnAmount--;
                    KitchenObject.SpawnKitchenObject(plateKitchenObjectSO, player);
                    OnPlateRemoved?.Invoke(this, EventArgs.Empty);
                }
            }
    }

}
