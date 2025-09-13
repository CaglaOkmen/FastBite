using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class RecipeCard : MonoBehaviour
{
    public GameObject[] gameObjects;
    public GameObject bunTop;
    public GameObject bunBottom;
    // Start is called before the first frame update
    void Start()
    {
        randomIngredients();
    }

    // Update is called once per frame
    void randomIngredients()
    {
        GameObject top = Instantiate(bunTop, transform);
        GameObject bottom = Instantiate(bunBottom, transform);

        top.transform.localScale = Vector3.one * 0.3f;
        top.transform.localPosition = new Vector2(0, gameObjects.Length);

        bottom.transform.localScale = Vector3.one * 0.3f;
        bottom.transform.localPosition = new Vector2(0, -2f);

        for (int i = 0; i < gameObjects.Length; i++)
        {
            var randomsayý = UnityEngine.Random.Range(0, gameObjects.Length);
            GameObject item = Instantiate(gameObjects[randomsayý], transform);
            item.transform.localScale = Vector3.one * 0.3f;

            var position = -1 + i;
            item.transform.localPosition = new Vector2(0, position);
            print(item.name + "eklendi");
        }

    }
}
