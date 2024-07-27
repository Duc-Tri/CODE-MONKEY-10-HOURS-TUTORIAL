using UnityEngine;

public interface IKitchenObjectParent 
{
    public Transform GetKitchenObjectFollowTransform();

    public void SetKitchenObject(KitchenObject ko);

    public KitchenObject GetKitchenObject();

    public void ClearKitchenObject();

    public bool HasKitchenObject();

}
