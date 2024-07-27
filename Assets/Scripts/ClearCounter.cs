using UnityEngine;

public class ClearCounter : BaseCounter
{
    [SerializeField] private KitchenObjectsSO kitchenObjectSO;

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab);
            kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(this);

            Debug.Log("INTERACT * " + transform.name + KitchenObject.GetKitchenObjectSO().objectName);
        }
        else
        {
            KitchenObject.SetKitchenObjectParent(player);

            Debug.Log("PICK ■ " + KitchenObject + " from " + KitchenObject.GetKitchenObjectParent());
        }
    }

}
