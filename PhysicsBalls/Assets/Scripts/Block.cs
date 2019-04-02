using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    private int hp;
    public int HP
    {
        get { return hp; }
        set
        {
            hp = value;
            textMesh.text = hp.ToString();
        }
    }
    TextMesh textMesh;

    // Start is called before the first frame update
    void Start()
    {
        textMesh = GetComponentInChildren<TextMesh>();
        HP = Random.Range(1, 10);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("enter!!!");
        HP -= 1;
        if (HP == 0)
            Destroy(gameObject);
    }
}
