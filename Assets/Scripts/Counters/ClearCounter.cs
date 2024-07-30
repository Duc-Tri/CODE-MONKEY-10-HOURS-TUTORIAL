using UnityEngine;

public class ClearCounter : BaseCounter
{
    [SerializeField] private KitchenObjectsSO kitchenObjectSO;

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

}
