using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEditor.Progress;
public class ItemMotion : MonoBehaviour
{
    float speed = 2f;
    GameObject[] gameObjects = new GameObject[0];

    private void Update()
    {
        if (gameObjects.Length == 0)
        {
            print("boþ");
        }
        else
        {
            for (int i = 0; i < gameObjects.Length; i++)
            {

                Vector3 movement = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
                gameObjects[i].transform.Translate(movement * speed * Time.deltaTime);
                print("hareket var");
            }
        }
    }
    public void randomItemMotion(GameObject[] items)
    {
        gameObjects = items;
    }
}
