using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter, IHasProgress
{

    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;

    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;

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

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
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

            Debug.Log("cuttingRecipe=" + cuttingRecipe);

            OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
            {
                progressNormalized = (float)cuttingProgress / cuttingRecipe.cuttingProgressMax
            });

            if (cuttingProgress >= cuttingRecipe.cuttingProgressMax)
            {
                KitchenObjectsSO outputSO = GetOutputForInput(KitchenObject.KitchenObjectSO);
                KitchenObject.DestroySelf();
                Debug.Log("=============== +" + KitchenObject);
                KitchenObject.SpawnKitchenObject(outputSO, this);
                Debug.Log("=============== ■" + KitchenObject);
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
