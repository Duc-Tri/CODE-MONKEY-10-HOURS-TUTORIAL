using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter
{

    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;

    public event EventHandler<OnProgressChangedEventArgs> OnProgressChanged;
    public class OnProgressChangedEventArgs : EventArgs
    {
        public float progressNormalized;
    }

    public event EventHandler OnCut;

    private int cuttingProgress;

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
                if (HasRecipeWithInput(player.KitchenObject.KitchenObjectSO))
                {
                    //player carries something which can be cut
                    player.KitchenObject.SetKitchenObjectParent(this);
                    cuttingProgress = 0;

                    CuttingRecipeSO cuttingRecipe = GetCuttingRecipeSOWithInput(KitchenObject.KitchenObjectSO);

                    OnProgressChanged?.Invoke(this, new OnProgressChangedEventArgs
                    {
                        progressNormalized = 0
                    });
                }
                Debug.Log("PLAYER DROP ■ " + KitchenObject);
            }
        }
    }

    public override void InteractAlternate(Player player)
    {
        if (HasKitchenObject() && HasRecipeWithInput(KitchenObject.KitchenObjectSO))
        {
            // Kitchen object present & can be cut !
            cuttingProgress++;

            OnCut?.Invoke(this, EventArgs.Empty);

            CuttingRecipeSO cuttingRecipe = GetCuttingRecipeSOWithInput(KitchenObject.KitchenObjectSO);

            OnProgressChanged?.Invoke(this, new OnProgressChangedEventArgs
            {
                progressNormalized = (float)cuttingProgress / cuttingRecipe.cuttingProgressMax
            });

            if (cuttingProgress >= cuttingRecipe.cuttingProgressMax)
            {
                KitchenObjectsSO output = GetOutputForInput(KitchenObject.KitchenObjectSO);
                KitchenObject.DestroySelf();

                KitchenObject.SpawnKitchenObject(output, this);
            }
        }
    }

    private bool HasRecipeWithInput(KitchenObjectsSO inputKitchenObjectSO)
    {
        return GetCuttingRecipeSOWithInput(inputKitchenObjectSO) != null;
    }

    private KitchenObjectsSO GetOutputForInput(KitchenObjectsSO inputKitchenObjectSO)
    {
        CuttingRecipeSO r = GetCuttingRecipeSOWithInput(inputKitchenObjectSO);
        return (r != null) ? r.output : null;
    }

    private CuttingRecipeSO GetCuttingRecipeSOWithInput(KitchenObjectsSO inputKitchenObjectSO)
    {
        foreach (var ckoSO in cuttingRecipeSOArray)
            if (ckoSO.input == inputKitchenObjectSO) return ckoSO;

        return null;
    }


}
