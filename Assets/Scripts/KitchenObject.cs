using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectsSO kitchenObjectSO;

    private ClearCounter clearCounter;

    public KitchenObjectsSO GetKitchenObjectSO()
    {
        return kitchenObjectSO;
    }

    public void SetClearCounter(ClearCounter counter)
    {
        if (clearCounter != null)
            clearCounter.ClearKitchenObject();

        clearCounter = counter;

        if (clearCounter.HasKitchenObject)
            Debug.LogError("COUNTER ALREADY HAS KITCHENOBJECT ! " + clearCounter.GetKitchenObject());

        clearCounter.SetKitchenObject(this);
        transform.parent = clearCounter.GetKitchenObjectFollowTransform();
        transform.localPosition = Vector3.zero;

        Debug.Log(this + " NEW COUNTER=" + clearCounter.name);
    }

    public ClearCounter GetClearCounter()
    {
        return clearCounter;
    }

}
