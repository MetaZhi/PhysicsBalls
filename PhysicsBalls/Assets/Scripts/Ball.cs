using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float Factor = 5;
    public bool isRunning = false;
    public int Attack = 1;
    Vector3 startPos;
    Rigidbody2D rigidbody;
    AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        source = GetComponent<AudioSource>();
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

        rigidbody.isKinematic = false;
        rigidbody.AddForce(dir.normalized * Factor, ForceMode2D.Impulse);

        isRunning = true;
    }

    void Reset()
    {
        rigidbody.velocity = Vector2.zero;
        rigidbody.angularVelocity = 0;
        rigidbody.isKinematic = true;

        isRunning = false;

        GetComponentInParent<BallManager>().ResetBall(gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision){
        source.PlayOneShot(source.clip);
    }
}
