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

        top.transform.localScale = Vector3.one * 0.3f;
        top.transform.localPosition = new Vector3(0, gameObjects.Length * 0.5f - 0.8f, gameObjects.Length * -0.02f);
        top.GetComponent<Collider2D>().enabled = false;

        bottom.transform.localScale = Vector3.one * 0.3f;
        bottom.transform.localPosition = new Vector3(0, -1.3f, -0.01f);
        bottom.GetComponent<Collider2D>().enabled = false;
        // icindekiler kismi rastgele uretiliyor.
        for (int i = 0; i < gameObjects.Length; i++)
        {
            var randomsayi = Random.Range(0, gameObjects.Length);
            GameObject item = Instantiate(gameObjects[randomsayi], transform);
            currentRecipe.Add(gameObjects[randomsayi]);
            item.transform.localScale = Vector3.one * 0.3f;

            var position = -1f + 0.5f * i;
            item.transform.localPosition = new Vector3(0, position, -0.02f + i * -0.01f);
            item.GetComponent<Collider2D>().enabled = false;

            print(item.name + "eklendi");
        }
        ItemMotion motion = FindObjectOfType<ItemMotion>();
        motion.randomItemMotion(currentRecipe.ToArray());
        
    }
}
