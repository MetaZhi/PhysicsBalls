using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiggerItem : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.GetComponent<Ball>().Attack == 1)
        {
            collider.gameObject.transform.localScale *= 1.5f;
            collider.GetComponent<Ball>().Attack = 2;
        }

        Destroy(gameObject);
    }
}
