using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RecipeCard : MonoBehaviour
{
    public GameObject[] gameObjects;
    public List<GameObject> currentRecipe = new List<GameObject>();
    
    public GameObject bunTop;
    public GameObject bunBottom;
    public GameObject cardBG;

    int itemCount;
    void Start()
    {
        randomIngredients();
        
    }
    
    void randomIngredients()
    {
        GameObject card = Instantiate(cardBG, transform);
        GameObject top = Instantiate(bunTop, transform);
        GameObject bottom = Instantiate(bunBottom, transform);
        card.transform.localPosition = new Vector2 (0, 0);

        int activePool = Mathf.Min(2 + (int)(GameManager.currentLevel / 2), gameObjects.Length);
        int maxItems = Mathf.Min(7, 2 + GameManager.currentLevel / 3);
        int minItems = Mathf.Max(2, maxItems - 2);
        itemCount = Random.Range(minItems, maxItems + 1);

        top.transform.localScale = Vector3.one * 0.3f;
        top.transform.localPosition = new Vector3(0, itemCount * 0.5f - 0.8f, itemCount * -0.02f);
        top.GetComponent<Collider2D>().enabled = false;

        bottom.transform.localScale = Vector3.one * 0.3f;
        bottom.transform.localPosition = new Vector3(0, -1.3f, -0.01f);
        bottom.GetComponent<Collider2D>().enabled = false;
        // icindekiler kismi rastgele uretiliyor.
        for (int i = 0; i < itemCount; i++)
        {
            var randomsayi = Random.Range(0, activePool);

            while (activePool > 1 && i >= 2 && 
                    currentRecipe[i - 1] == gameObjects[randomsayi] && 
                    currentRecipe[i - 2] == gameObjects[randomsayi])
            {
                randomsayi = Random.Range(0, activePool);
            }

            GameObject item = Instantiate(gameObjects[randomsayi], transform);
            currentRecipe.Add(gameObjects[randomsayi]);
            item.transform.localScale = Vector3.one * 0.3f;

            var position = -1f + 0.5f * i;
            item.transform.localPosition = new Vector3(0, position, -0.02f + i * -0.01f);
            item.GetComponent<Collider2D>().enabled = false;
        }

        int extraCount = 2 + (int)(GameManager.currentLevel / 5);
        List<GameObject> Items = new List<GameObject>(currentRecipe);
        for (int i = 0; i < extraCount; i++)
        {
            int randomExtra = Random.Range(0, activePool);
            Items.Add(gameObjects[randomExtra]);
        }
        ItemMotion motion = FindObjectOfType<ItemMotion>();
        motion.randomItemMotion(Items.ToArray());
        
    }

    public void GenerateNewRecipe()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        currentRecipe.Clear();
        randomIngredients();
    }
}
