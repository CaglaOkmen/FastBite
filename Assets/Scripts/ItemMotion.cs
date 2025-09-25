using System.Collections.Generic;
using UnityEngine;
public class ItemMotion : MonoBehaviour
{
    float speed = 2f;
    Rigidbody2D[] rb;
    List<Vector3> mov = new List<Vector3>();
    Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

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
                var camRB = cam.WorldToViewportPoint(rb[i].position);
                // kenarlarda takýlma oluyor
                if (camRB.x <= 0.03f || camRB.x >= 0.65f || camRB.y <= 0.05f || camRB.y >= 0.95f)
                {
                    rb[i].velocity = Vector2.zero;
                    ChangeVec(camRB, i);
                    print("Yon degisti");
                    rb[i].velocity = mov[i];
                }
                else
                {
                    rb[i].velocity = mov[i] * speed;
                }

                //týklanma sorunu var
                if (Input.GetMouseButton(0))
                {
                    rb[i].velocity = Vector2.zero;
                    print("týklandý");
                    Vector3 mausePosition = cam.ScreenToWorldPoint(Input.mousePosition);
                    var ofset = rb[i].transform.position - mausePosition;

                    rb[i].position = mausePosition - ofset;
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
            rb[i].transform.localPosition = Vector3.zero;
            mov.Add(new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f));
            print("rb & mov uretildi");
        }
    }
}
