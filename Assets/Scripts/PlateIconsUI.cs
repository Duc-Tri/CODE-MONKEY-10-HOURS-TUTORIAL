using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateIconsUI : MonoBehaviour
{
    [SerializeField] private PlateKitchenObject plateKitchenObject;
    [SerializeField] private Transform iconTemplate;

    private void Start()
    {
        plateKitchenObject.OnIngredientAdded += PlateKitchenObject_OnIngredientAdded;
        iconTemplate.gameObject.SetActive(false);
    }

    private void PlateKitchenObject_OnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e)
    {
        UpdateVisuel();
    }

    private void UpdateVisuel()
    {
        foreach (Transform child in transform)
        {
            if (child != iconTemplate)
                Destroy(child.gameObject);
        }

        foreach (var koSO in plateKitchenObject.GetKitchenObjectSOList())
        {
            Transform iconTransform = Instantiate(iconTemplate, this.transform);
            iconTransform.gameObject.SetActive(true);
            iconTransform.GetComponent<PlateIconsSingleUI>().SetKitchenObjectSO(koSO);
        }
    }

}
