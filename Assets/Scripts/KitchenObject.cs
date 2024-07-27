using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectsSO kitchenObjectSO;

    private IKitchenObjectParent kitchenObjectParent;

    public KitchenObjectsSO GetKitchenObjectSO() => kitchenObjectSO;

    public void SetKitchenObjectParent(IKitchenObjectParent kop)
    {
        if (kitchenObjectParent != null)
            kitchenObjectParent.ClearKitchenObject();

        kitchenObjectParent = kop;

        if (kitchenObjectParent.HasKitchenObject())
            Debug.LogError("COUNTER ALREADY HAS KITCHENOBJECT ! " + kitchenObjectParent.GetKitchenObject());

        kitchenObjectParent.SetKitchenObject(this);
        transform.parent = kitchenObjectParent.GetKitchenObjectFollowTransform();
        transform.localPosition = Vector3.zero;

        Debug.Log(this + " NEW COUNTER=" + kitchenObjectParent);
    }

    public IKitchenObjectParent GetKitchenObjectParent() => kitchenObjectParent;

}
