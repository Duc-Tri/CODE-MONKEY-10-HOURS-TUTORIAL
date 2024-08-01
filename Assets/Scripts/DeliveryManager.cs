using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    public static DeliveryManager Instance { get;private set; }

    [SerializeField] private RecipeListSO recipeListSO;

    public List<RecipeSO> waitingRecipeSOList;

    private float spawnRecipeTimer;
    private float spawnRecipeTimerMax = 3;
    private int waitingRecipesMax = 4;

    private List<RecipeSO> possibleRecipes => recipeListSO.recipeSOList;

    private void Awake()
    {
        Instance = this;
        waitingRecipeSOList = new List<RecipeSO>();
    }

    private void Update()
    {
        if (waitingRecipeSOList.Count < waitingRecipesMax)
        {
            spawnRecipeTimer -= Time.deltaTime;
            if (spawnRecipeTimer < 0)
            {
                spawnRecipeTimer = spawnRecipeTimerMax;

                RecipeSO waitingRecipeSO = possibleRecipes[Random.Range(0, possibleRecipes.Count)];
                Debug.Log("waitingRecipeSO=" + waitingRecipeSO.name);
                waitingRecipeSOList.Add(waitingRecipeSO);
            }
        }
    }

    public void DeliverRecipe(PlateKitchenObject plateKitchenObject)
    {
        for (int i = 0; i < waitingRecipeSOList.Count; i++)
        {
            RecipeSO wRSO = waitingRecipeSOList[i];
            if (wRSO.kitchenObjectSOList.Count == plateKitchenObject.GetKitchenObjectSOList().Count)
            {
                // has same number of ingredients
                bool plateContentMatchesRecipe = true;
                foreach (KitchenObjectSO koSO in wRSO.kitchenObjectSOList)
                {
                    // cycling through all ingredients in the recipe
                    bool ingredientFound = false;
                    foreach (KitchenObjectSO pKoSO in plateKitchenObject.GetKitchenObjectSOList())
                    {
                        // cycling through all ingredients on the plate
                        if (pKoSO == koSO)
                        {
                            // ingredients match !
                            ingredientFound = true;
                            break;
                        }
                    }

                    if (!ingredientFound)
                    {
                        // recipe ingredient not found on the plate
                        plateContentMatchesRecipe = false;
                        break;
                    }
                }

                if (plateContentMatchesRecipe)
                {
                    // player delivered one correct recipe
                    Debug.Log("player delivered one correct recipe " + wRSO.recipeName);
                    waitingRecipeSOList.RemoveAt(i);
                    return;
                }
            }
        }

        // No matches found !

        // Player did not deliver a correct recipe
        Debug.Log("PLAYER DELIVERED INCORRECT RECIPE !!! ");
    }

}
