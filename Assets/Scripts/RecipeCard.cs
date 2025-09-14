using System.Linq;
using UnityEngine;
using static UnityEditor.Progress;

public class RecipeCard : MonoBehaviour
{
    public GameObject[] gameObjects;
    public GameObject bunTop;
    public GameObject bunBottom;
    public GameObject cardBG;
    GameObject[] items = new GameObject[0];

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
        top.transform.localPosition = new Vector3(0, gameObjects.Length * 0.5f - 0.8f, -1f);

        bottom.transform.localScale = Vector3.one * 0.3f;
        bottom.transform.localPosition = new Vector3(0, -1.3f, 0);

        for (int i = 0; i < gameObjects.Length; i++)
        {
            var randomsayý = UnityEngine.Random.Range(0, gameObjects.Length);
            GameObject item = Instantiate(gameObjects[randomsayý], transform);
            items = items.Append(item).ToArray();
            item.transform.localScale = Vector3.one * 0.3f;

            var position = -1f + 0.5f * i;
            item.transform.localPosition = new Vector3(0, position, i * -0.1f);

            print(item.name + "eklendi");
        }
          
        
    }

    public GameObject[] ObjList()
    {
        return items;
    }
}
