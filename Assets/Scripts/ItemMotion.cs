using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ItemMotion : MonoBehaviour
{
    float speed = 2f;
    Rigidbody2D[] rb;
    List<Vector3> mov = new List<Vector3>();

    private void Update()
    {
        if (rb.Length == 0)
        {
            print("boþ");
        }
        else
        {
            for (int i = 0; i < rb.Length; i++)
            {
                // kenara çarpýp yön deðiþtirilecek
                rb[i].AddForce(speed * mov[i]);
                print("hareket var");
            }
        }
    }
    public void randomItemMotion(GameObject[] items)
    {
        rb = new Rigidbody2D[items.Length];
        mov.Clear();

        for (int i = 0; i < items.Length; i++)
        {
            items[i] = Instantiate(items[i], transform);
            rb[i] = items[i].GetComponent<Rigidbody2D>();
            mov.Add(new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f));
            print("rb & mov uretildi");
        }
    }
}
