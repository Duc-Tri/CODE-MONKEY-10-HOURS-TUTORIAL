using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectsSO kitchenObjectSO;

    public KitchenObjectsSO GetKitchenObjectSO()
    {
        return kitchenObjectSO;
    }
}

