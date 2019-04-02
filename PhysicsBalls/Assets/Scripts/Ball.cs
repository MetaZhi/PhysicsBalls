using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float Factor = 5;
    public bool isRunning = false;
    Vector3 startPos;
    Rigidbody2D rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -4)
        {
            Reset();
        }
    }

    public void Shoot(Vector3 startPos)
    {
        transform.position = startPos;
        Vector3 mousePos = Input.mousePosition;

        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        Debug.Log(worldPos);

        worldPos.z = 0;

        Vector2 dir = worldPos - transform.position;

        rigidbody.AddForce(dir.normalized * Factor, ForceMode2D.Impulse);
        rigidbody.gravityScale = 1;

        isRunning = true;
    }

    void Reset()
    {
        transform.position = startPos;
        rigidbody.velocity = Vector2.zero;
        rigidbody.angularVelocity = 0;
        rigidbody.gravityScale = 0;

        isRunning = false;
    }
}
