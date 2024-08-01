using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static CuttingCounter;

public class StoveCounter : BaseCounter, IHasProgress
{
    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;

    public event EventHandler<OnStateChangedEventArgs> OnStateChanged;
    public class OnStateChangedEventArgs : EventArgs
    {
        public State State;
    }

    public enum State { Idle, Frying, Fried, Burned }

    [SerializeField] private FryingRecipeSO[] fryingRecipeSOArray;
    [SerializeField] private BurningRecipeSO[] burningRecipeSOArray;

    private State state;


    private float fryingTimer, burningTimer;
    private FryingRecipeSO fryingRecipeSO;
    private BurningRecipeSO burningRecipeSO;

    private void Start()
    {
        state = State.Idle;
    }

    private void Update()
    {
        if (HasKitchenObject())
        {
            switch (state)
            {
                case (State.Idle):
                    break;

                case (State.Frying):
                    fryingTimer += Time.deltaTime;

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = fryingTimer / fryingRecipeSO.fryingTimerMax
                    });

                    if (fryingTimer > fryingRecipeSO.fryingTimerMax)
                    {
                        // FRIED
                        fryingTimer = 0;
                        KitchenObject.DestroySelf();

                        KitchenObject.SpawnKitchenObject(fryingRecipeSO.output, this);

                        Debug.Log("Fried !");

                        state = State.Fried;
                        burningTimer = 0;
                        burningRecipeSO = GetBurningRecipeSOWithInput(KitchenObject.KitchenObjectSO);

                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                        {
                            State = state
                        });
                    }
                    break;

                case (State.Fried):
                    burningTimer += Time.deltaTime;

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = burningTimer / burningRecipeSO.burningTimerMax
                    });

                    if (burningTimer > burningRecipeSO.burningTimerMax)
                    {
                        // BURNED
                        burningTimer = 0;
                        KitchenObject.DestroySelf();

                        KitchenObject.SpawnKitchenObject(burningRecipeSO.output, this);

                        state = State.Burned;

                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                        {
                            State = state
                        });

                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                        {
                            progressNormalized = 0
                        });

                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                        {
                            progressNormalized = 0
                        });

                        Debug.Log("Burned !");
                    }
                    break;

                case (State.Burned):
                    break;
            }

            //Debug.Log(state + " / " + fryingTimer + " / " + burningTimer);

        }
    }

    public override void Interact(Player player)
    {
        if (HasKitchenObject()) // === ONE OBJECT ON COUNTER
        {
            if (player.HasKitchenObject()) // --- player carries something
            {
                if (player.KitchenObject.TryGetPlate(out PlateKitchenObject plateKitchenObject)) // a plate !
                {
                    if (plateKitchenObject.TryAddIngredient(this.KitchenObject.KitchenObjectSO))
                        KitchenObject.DestroySelf();

                    state = State.Idle;

                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                    {
                        State = state
                    });

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = 0
                    });
                }
            }
            else // --- player carries nothing
            {
                KitchenObject.SetKitchenObjectParent(player);
                state = State.Idle;

                OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                {
                    State = state
                });

                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                {
                    progressNormalized = 0
                });

                Debug.Log("PLAYER PICKUP ■ " + KitchenObject);
            }
        }
        else // === NO OBJECT ON COUNTER
        {
            if (player.HasKitchenObject()) // --- player carries something
            {
                if (HasRecipeWithInput(player.KitchenObject.KitchenObjectSO))
                {
                    // player carries something which can be cooked
                    player.KitchenObject.SetKitchenObjectParent(this);
                    fryingRecipeSO = GetFryingRecipeSOWithInput(KitchenObject.KitchenObjectSO);
                    state = State.Frying;
                    fryingTimer = 0;

                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                    {
                        State = state
                    });

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = fryingTimer / fryingRecipeSO.fryingTimerMax
                    });
                }
                Debug.Log("PLAYER DROP ■ " + KitchenObject);
            }
        }
    }

    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        return GetFryingRecipeSOWithInput(inputKitchenObjectSO) != null;
    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO)
    {
        FryingRecipeSO f = GetFryingRecipeSOWithInput(inputKitchenObjectSO);
        return (f != null) ? f.output : null;
    }

    private FryingRecipeSO GetFryingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (var f in fryingRecipeSOArray)
            if (f.input == inputKitchenObjectSO) return f;

        return null;
    }

    private BurningRecipeSO GetBurningRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (var b in burningRecipeSOArray)
            if (b.input == inputKitchenObjectSO) return b;

        return null;
    }

}
