using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent
{
    [SerializeField] private Transform counterTopPoint;

    private KitchenObject kitchenObject;
    public KitchenObject KitchenObject
    //{ get; set; }
    { get => kitchenObject; set => kitchenObject = value; }

    public virtual void Interact(Player player)
    {
        Debug.LogError("BaseCounter Interact !");
    }

    public virtual void InteractAlternate(Player player)
    {
        Debug.LogError("BaseCounter Interact Alternate!");
    }

    // ========== IKitchenObjectParent
    public Transform GetKitchenObjectFollowTransform()
    {
        return counterTopPoint;
    }

    public void ClearKitchenObject()
    {
        KitchenObject = null;
    }

    public bool HasKitchenObject() => (KitchenObject != null);

}
