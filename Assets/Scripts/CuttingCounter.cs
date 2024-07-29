using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
    [SerializeField] private CuttingRecipeSO[] cuttingKitchenObjectsSOArray;

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
                if (HasRecipeWithInput(player.KitchenObject.GetKitchenObjectSO()))
                {
                    //player carries something which can be cut
                    player.KitchenObject.SetKitchenObjectParent(this);
                }
                Debug.Log("PLAYER DROP ■ " + KitchenObject);
            }
        }
    }

    public override void InteractAlternate(Player player)
    {
        if (HasKitchenObject() && HasRecipeWithInput(KitchenObject.GetKitchenObjectSO()))
        {
            // object can be cut !
            KitchenObjectsSO output = GetOutputForInput(KitchenObject.GetKitchenObjectSO());
            KitchenObject.DestroySelf();

            KitchenObject.SpawnKitchenObject(output, this);
        }
    }

    private bool HasRecipeWithInput(KitchenObjectsSO kitchenObjectsSO)
    {
        foreach (var ckoSO in cuttingKitchenObjectsSOArray)
            if (ckoSO.input == kitchenObjectsSO) return true;

        return false;
    }

    private KitchenObjectsSO GetOutputForInput(KitchenObjectsSO inputKitchenObjectSO)
    {
        foreach (var ckoSO in cuttingKitchenObjectsSOArray)
            if (ckoSO.input == inputKitchenObjectSO) return ckoSO.output;

        return null;
    }

}
