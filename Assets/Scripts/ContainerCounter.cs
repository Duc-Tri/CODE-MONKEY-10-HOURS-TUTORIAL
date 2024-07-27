using System;
using UnityEngine;

// Spawn ingredient to player
public class ContainerCounter : BaseCounter
{
    public event EventHandler OnPlayerGrabberObject;

    [SerializeField] private KitchenObjectsSO kitchenObjectSO;

    public override void Interact(Player player)
    {
        Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab);
        kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(player);
        OnPlayerGrabberObject?.Invoke(this, EventArgs.Empty);

        Debug.Log("INTERACT * " + transform.name);
    }

}
