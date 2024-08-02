using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryManagerSingleUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI recipeNameText;
    [SerializeField] private Transform iconContainer;
    [SerializeField] private Transform iconTemplate;

    private void Awake()
    {
        iconTemplate.gameObject.SetActive(false);
    }

    public void SetRecipeSO(RecipeSO recipeSO)
    {
        recipeNameText.text = recipeSO.recipeName;

        foreach (Transform child in iconContainer)
        {
            if (child != iconTemplate)
                Destroy(child.gameObject);
        }

        foreach (KitchenObjectSO koso in recipeSO.kitchenObjectSOList)
        {
            Transform iconTrans = Instantiate(iconTemplate, iconContainer);
            iconTrans.gameObject.SetActive(true);
            iconTrans.GetComponent<Image>().sprite = koso.sprite;
        }
    }
}
