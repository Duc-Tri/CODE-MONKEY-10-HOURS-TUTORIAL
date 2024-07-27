using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour
{
    [SerializeField] private KitchenObjectsSO kitchenObjectSO;
    [SerializeField] private Transform counterTopPoint;
    [SerializeField] private ClearCounter secondCounter;
    [SerializeField] private bool testing;

    private KitchenObject kitchenObject;

    private void Update()
    {
        // testing
        if (testing && Input.GetKeyDown(KeyCode.T) && kitchenObject != null)
        {
            kitchenObject.SetClearCounter(secondCounter);
        }
    }

    public void Interact()
    {
        if (kitchenObject == null)
        {
            Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab, counterTopPoint);

            kitchenObjectTransform.localPosition = Vector3.zero;
            kitchenObjectTransform.GetComponent<KitchenObject>().SetClearCounter(this);

            Debug.Log("INTERACT ■ " + transform.name + kitchenObject.GetKitchenObjectSO().objectName);
        }
        else
        {
            Debug.Log("K-O: " + kitchenObject + " on " + kitchenObject.GetClearCounter());
        }
    }

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

    public bool HasKitchenObject => (kitchenObject != null);

}
