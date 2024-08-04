using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter, IHasProgress
{
    public static event EventHandler OnAnyCut;

    public new static void ResetStaticData()
    {
        OnAnyCut = null;
    }

    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;
    public event EventHandler OnCut;

    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;

    private int cuttingProgress;

    public override void Interact(Player player)
    {
        if (HasKitchenObject()) // === ON OBJECT ON COUNTER
        {
            if (player.HasKitchenObject()) // --- player carries something
            {
                if (player.KitchenObject.TryGetPlate(out PlateKitchenObject plateKitchenObject)) // a plate !
                {
                    if (plateKitchenObject.TryAddIngredient(this.KitchenObject.KitchenObjectSO))
                        KitchenObject.DestroySelf();
                }
            }
            else // --- player carries nothing
            {
                KitchenObject.SetKitchenObjectParent(player);
                Debug.Log("PLAYER PICKUP ■ " + KitchenObject);
            }
        }
        else // === NO OBJECT ON COUNTER
        {

            if (player.HasKitchenObject()) // --- player carries something
            {
                if (HasRecipeWithInput(player.KitchenObject.KitchenObjectSO))
                {
                    // player carries something which can be cut
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
            OnAnyCut?.Invoke(this, EventArgs.Empty);

            Debug.Log("OnAnyCut.GetInvocationList().Length=" + OnAnyCut.GetInvocationList().Length);

            CuttingRecipeSO cuttingRecipe = GetCuttingRecipeSOWithInput(KitchenObject.KitchenObjectSO);

            Debug.Log("cuttingRecipe=" + cuttingRecipe);

            OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
            {
                progressNormalized = (float)cuttingProgress / cuttingRecipe.cuttingProgressMax
            });

            if (cuttingProgress >= cuttingRecipe.cuttingProgressMax)
            {
                KitchenObjectSO outputSO = GetOutputForInput(KitchenObject.KitchenObjectSO);
                KitchenObject.DestroySelf();
                Debug.Log("=============== +" + KitchenObject);
                KitchenObject.SpawnKitchenObject(outputSO, this);
                Debug.Log("=============== ■" + KitchenObject);
            }
        }
    }

    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        return GetCuttingRecipeSOWithInput(inputKitchenObjectSO) != null;
    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO)
    {
        CuttingRecipeSO r = GetCuttingRecipeSOWithInput(inputKitchenObjectSO);
        return (r != null) ? r.output : null;
    }

    private CuttingRecipeSO GetCuttingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (var ckoSO in cuttingRecipeSOArray)
            if (ckoSO.input == inputKitchenObjectSO) return ckoSO;

        return null;
    }

}
