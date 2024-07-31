using UnityEngine;

public class ClearCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    public override void Interact(Player player)
    {
        if (HasKitchenObject()) // === ONE OBJECT ON COUNTER
        {
            if (player.HasKitchenObject()) // --- player carries something
            {
                if (player.KitchenObject.TryGetPlate(out PlateKitchenObject plateKitchenObject)) // ::: player has a plate !
                {
                    if (plateKitchenObject.TryAddIngredient(this.KitchenObject.KitchenObjectSO))
                        this.KitchenObject.DestroySelf();
                }
                else // ::: player doesnt have not a plate !
                {
                    if (this.KitchenObject.TryGetPlate(out plateKitchenObject)) // counter has a plate !
                        if (plateKitchenObject.TryAddIngredient(player.KitchenObject.KitchenObjectSO))
                            player.KitchenObject.DestroySelf();
                }
            }
            else // --- player carries nothing
            {
                this.KitchenObject.SetKitchenObjectParent(player);
                Debug.Log("PLAYER PICKUP ■ " + KitchenObject);
            }
        }
        else // === NO OBJECT ON COUNTER
        {
            if (player.HasKitchenObject()) // --- player carries something
            {
                player.KitchenObject.SetKitchenObjectParent(this);
                Debug.Log("PLAYER DROP ■ " + KitchenObject);
            }
            else // --- player carries nothing
            {

            }
        }
    }

}
