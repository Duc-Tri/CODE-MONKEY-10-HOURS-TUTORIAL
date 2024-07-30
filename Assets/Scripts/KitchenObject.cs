using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectsSO kitchenObjectSO;

    private IKitchenObjectParent kitchenObjectParent;

    public KitchenObjectsSO KitchenObjectSO => kitchenObjectSO;

    public void SetKitchenObjectParent(IKitchenObjectParent kop)
    {
        if (kitchenObjectParent != null)
            kitchenObjectParent.ClearKitchenObject();

        kitchenObjectParent = kop;

        if (kitchenObjectParent.HasKitchenObject())
            Debug.LogError("COUNTER ALREADY HAS KITCHENOBJECT ! " + kitchenObjectParent.KitchenObject);

        kitchenObjectParent.KitchenObject = this;
        transform.parent = kitchenObjectParent.GetKitchenObjectFollowTransform();
        transform.localPosition = Vector3.zero;

        Debug.Log(this + " NEW COUNTER=" + kitchenObjectParent);
    }

    public IKitchenObjectParent GetKitchenObjectParent() => kitchenObjectParent;

    public void DestroySelf()
    {
        kitchenObjectParent.ClearKitchenObject();
        Destroy(gameObject);
    }

    public static KitchenObject SpawnKitchenObject(KitchenObjectsSO kitchenObjectSO, IKitchenObjectParent kitchenObjectParent)
    {
        Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab);
        KitchenObject kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();
        kitchenObject.SetKitchenObjectParent(kitchenObjectParent);

        return kitchenObject;
    }

}
