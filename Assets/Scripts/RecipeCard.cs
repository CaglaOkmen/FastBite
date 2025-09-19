using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RecipeCard : MonoBehaviour
{
    public GameObject[] gameObjects;
    public GameObject bunTop;
    public GameObject bunBottom;
    public GameObject cardBG;

    void Start()
    {
        randomIngredients();
        
    }

    
    void randomIngredients()
    {
        GameObject[] list = new GameObject[0];
        GameObject card = Instantiate(cardBG, transform);
        GameObject top = Instantiate(bunTop, transform);
        GameObject bottom = Instantiate(bunBottom, transform);
        card.transform.localPosition = new Vector2 (0, 0);

        top.transform.localScale = Vector3.one * 0.3f;
        top.transform.localPosition = new Vector3(0, gameObjects.Length * 0.5f - 0.8f, gameObjects.Length * -0.02f);

        bottom.transform.localScale = Vector3.one * 0.3f;
        bottom.transform.localPosition = new Vector3(0, -1.3f, 0);

        // içindekiler kýsmý rastgele üretiliyor.
        for (int i = 0; i < gameObjects.Length; i++)
        {
            var randomsayý = Random.Range(0, gameObjects.Length);
            GameObject item = Instantiate(gameObjects[randomsayý], transform);
            list = list.Append(gameObjects[randomsayý]).ToArray();
            item.transform.localScale = Vector3.one * 0.3f;

            var position = -1f + 0.5f * i;
            item.transform.localPosition = new Vector3(0, position, -0.01f + i * -0.01f);

            print(item.name + "eklendi");
        }
        print(list[0].name + "\n" + list[1].name);
        ItemMotion motion = FindObjectOfType<ItemMotion>();
        motion.randomItemMotion(list);
        
    }
}
