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
        Ball ball = collision.gameObject.GetComponent<Ball>();
        int deductHp = ball.Attack;
        // 如果砖块的血量小于小球的攻击力，只扣除小球的血量
        if (HP < ball.Attack)
            deductHp = HP;

        HP -= deductHp;
        // 加分
        GameManager.Instance.AddScore(deductHp);
        if (HP == 0)
            Destroy(gameObject);
    }
}
