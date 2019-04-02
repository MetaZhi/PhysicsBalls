using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float Factor = 5;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 计算鼠标抬起时的方向
        if (Input.GetMouseButtonUp(0)){
            Vector3 mousePos = Input.mousePosition;

            Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
            Debug.Log(worldPos);

            worldPos.z = 0;

            Vector2 dir = worldPos - transform.position;

            GetComponent<Rigidbody2D>().AddForce(dir.normalized * Factor, ForceMode2D.Impulse);
        }

    }
}
