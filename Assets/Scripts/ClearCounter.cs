using UnityEngine;

public class ClearCounter : MonoBehaviour, IKitchenObjectParent
{
    [SerializeField] private KitchenObjectsSO kitchenObjectSO;
    [SerializeField] private Transform counterTopPoint;

    private KitchenObject kitchenObject;

    public void Interact(Player player)
    {
        if (kitchenObject == null)
        {
            Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab, counterTopPoint);

            kitchenObjectTransform.localPosition = Vector3.zero;
            kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(this);

            Debug.Log("INTERACT * " + transform.name + kitchenObject.GetKitchenObjectSO().objectName);
        }
        else
        {
            Debug.Log("PICK ■ " + kitchenObject + " from " + kitchenObject.GetKitchenObjectParent());
            kitchenObject.SetKitchenObjectParent(player);
        }
    }

    // ========== IKitchenObjectParent
    public Transform GetKitchenObjectFollowTransform()
    {
        return counterTopPoint;
    }

    public void SetKitchenObject(KitchenObject ko)
    {
        kitchenObject = ko;
        Debug.Log(this + " ► SetKitchenObject ► " + kitchenObject);
    }

    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }

    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }

    public bool HasKitchenObject() => (kitchenObject != null);

}
