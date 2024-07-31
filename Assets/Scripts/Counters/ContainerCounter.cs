using System;
using UnityEngine;

// Spawn ingredient to player
public class ContainerCounter : BaseCounter
{
    public event EventHandler OnPlayerGrabberObject;

    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    public override void Interact(Player player)
    {
        // player carries nothing
        if (!player.HasKitchenObject())
        {
            KitchenObject.SpawnKitchenObject(kitchenObjectSO, player);

            OnPlayerGrabberObject?.Invoke(this, EventArgs.Empty);

            Debug.Log("PLAYER GOT A NEW * " + player.KitchenObject);
        }
    }

}
