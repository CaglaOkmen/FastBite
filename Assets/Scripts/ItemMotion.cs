using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ItemMotion : MonoBehaviour
{
    float speed = 3f;

    // Start is called before the first frame update
    private void Start()
    {
        
    }


    // Update is called once per frame
    private void Update()
    {
        
    }

    public void randomItemMotion(GameObject[] items)
    {
        for (int i = 0; i < items.Length; i++)
        {
            GameObject item = Instantiate(items[i], transform);
            var randomHorizontal = Random.Range(-5, 5);
            var randomVertical = Random.Range(-5, 5);
            item.transform.localPosition = new Vector3(randomHorizontal * speed * Time.deltaTime, randomVertical * speed * Time.deltaTime);
        }
    }
}
