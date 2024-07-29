using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
    [SerializeField] private KitchenObjectsSO cuKitchenObjectsSO;

    public override void Interact(Player player)
    {
        if (HasKitchenObject()) // some kitchen object at top
        {
            // player carries nothing
            if (!player.HasKitchenObject())
            {
                KitchenObject.SetKitchenObjectParent(player);
                Debug.Log("PLAYER PICKUP ■ " + KitchenObject);
            }
        }
        else // no object on counter
        {
            // player carries something
            if (player.HasKitchenObject())
            {
                player.KitchenObject.SetKitchenObjectParent(this);
                Debug.Log("PLAYER DROP ■ " + KitchenObject);
            }
        }
    }

    public override void InteractAlternate(Player player)
    {
        if (HasKitchenObject())
        {
            KitchenObject.DestroySelf();
            KitchenObject.SpawnKitchenObject(cuKitchenObjectsSO, this);
        }
    }
}
