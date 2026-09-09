using System.Collections.Generic;
using UnityEngine;

public class ItemMotion : MonoBehaviour
{
    float speed = 2f;
    Rigidbody2D[] rb;
    List<Vector3> mov = new List<Vector3>();
    Camera cam;
    
    public GameObject selectedItem;

    void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        if (rb.Length == 0)
        {
            Debug.Log("bos");
        }
        else
        {
            for (int i = 0; i < rb.Length; i++)
            {
                if (rb[i] == null || rb[i].bodyType == RigidbodyType2D.Static || rb[i].gameObject == selectedItem) continue;

                var camRB = cam.WorldToViewportPoint(rb[i].position);
                // kenarlarda takilma oluyor
                if (camRB.x <= 0.03f || camRB.x >= 0.65f || camRB.y <= 0.05f || camRB.y >= 0.95f)
                {
                    rb[i].linearVelocity = Vector2.zero;
                    ChangeVec(camRB, i);
                    rb[i].linearVelocity = mov[i];
                }
                else
                {
                    rb[i].linearVelocity = mov[i] * speed;
                }
            }
        }
    }

    public void ChangeVec(Vector3 vec, int i)
    {
        if (vec.x > 0.64)
        {
            mov[i] = new Vector3(Random.Range(-1f, 0f), mov[i].y, 0f).normalized;
        }
        else if (vec.x < 0.04)
        {
            mov[i] = new Vector3(Random.Range(0f, 1f), mov[i].y, 0f).normalized;
        }
        else
        {
            mov[i] = new Vector3(Random.Range(-1f, 1f), mov[i].y, 0f).normalized;
        }
        if (vec.y > 0.94)
        {
            mov[i] = new Vector3(mov[i].x, Random.Range(-1f, 0f), 0f).normalized;
        }
        else if (vec.y < 0.04)
        {
            mov[i] = new Vector3(mov[i].x, Random.Range(0f, 1f), 0f).normalized;
        }
        else
        {
            mov[i] = new Vector3(mov[i].x, Random.Range(-1f, 1f), 0f).normalized;
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
            rb[i].transform.localScale = Vector3.one * 0.2f;
            rb[i].transform.localPosition = new Vector3(Random.Range(-5f, 5f), Random.Range(-4f, 4f), -5f);
            mov.Add(new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f));
        }
    }
    
    public void SetSelectedItem(GameObject item)
    {
        selectedItem = item;
    }
}