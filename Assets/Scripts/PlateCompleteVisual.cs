using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCompleteVisual : MonoBehaviour
{
    [Serializable]
    public struct KitcehnObjectSO_GameObject
    {
        public KitchenObjectSO kitchenObjectSO;
        public GameObject gameObject;
    }

    [SerializeField] private PlateKitchenObject plateKitchenObject;
    [SerializeField] private List<KitcehnObjectSO_GameObject> kitcehnObjectSOGameObjectsList;

    private void Start()
    {
        plateKitchenObject.OnIngredientAdded += PlateKitchenObject_OnIngredientAdded;

        foreach (var kSOgo in kitcehnObjectSOGameObjectsList)
            kSOgo.gameObject.SetActive(false);
    }

    private void PlateKitchenObject_OnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e)
    {
        foreach (var kSOgo in kitcehnObjectSOGameObjectsList)
            if (kSOgo.kitchenObjectSO == e.kitchenObjectSO)
                kSOgo.gameObject.SetActive(true);
    }

}
