using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour
{
    public LineRenderer Line;
    public PhysicsMaterial2D Bounce;
    public PhysicsMaterial2D NoBounce;
    public Transform LeftBottom;
    public Transform LeftTop;
    public Transform LeftEnd;
    public Transform RightBottom;
    public Transform RightTop;
    public Transform RightEnd;
    Vector3 startPos;

    public static BallManager Instance { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        startPos = transform.GetChild(0).position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0) && GameManager.Instance.canShoot)
        {
            Line.enabled = false;
            StartCoroutine(ShootBalls());
        }

        if (Input.GetMouseButtonDown(0)) {
            Line.enabled = true;
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Debug.Log(worldPos);

            worldPos.z = 0;
            Line.SetPosition(1, worldPos);
        }
    }

    IEnumerator ShootBalls()
    {
        // 如果还有球在running，就不发射
        foreach (Transform ball in transform)
        {
            if (ball.GetComponent<Ball>().isRunning)
                yield break;
        }

        Physics2D.IgnoreLayerCollision(9, 9);

        // 逐个发射小球
        foreach (Transform ball in transform)
        {
            if (ball.GetComponent<Ball>().isRunning)
                continue;

            ball.GetComponent<Collider2D>().sharedMaterial = Bounce;
            ball.GetComponent<Ball>().Shoot(startPos);
            yield return new WaitForSeconds(0.5f);
        }
    }

    internal void ResetBalls()
    {
        for (int i = 3; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }

        foreach (Transform item in transform)
        {
            item.GetComponent<Ball>().Restart();
        }
    }

    public void OnBallReset()
    {
        if (IsAllReset())
        {
            Debug.Log("AllReset");
            BlockManager.Instance.RaiseBlocks();
        }
    }

    bool IsAllReset()
    {
        foreach (Transform ball in transform)
        {
            if (ball.GetComponent<Ball>().isRunning)
                return false;
        }

        return true;
    }

    public void ResetBall(GameObject ball)
    {
        bool isLeft = ball.transform.position.x < 0;

        var seq = LeanTween.sequence();
        if (isLeft)
        {
            seq.append(LeanTween.move(ball, LeftBottom, 0.3f));
            seq.append(LeanTween.move(ball, LeftTop, 0.6f));
            seq.append(LeanTween.move(ball, LeftEnd, 0.1f));
        }
        else
        {
            seq.append(LeanTween.move(ball, RightBottom, 0.3f));
            seq.append(LeanTween.move(ball, RightTop, 0.6f));
            seq.append(LeanTween.move(ball, RightEnd, 0.1f));
        }

        seq.append(() =>
        {
            ball.GetComponent<Ball>().isRunning = false;
            ball.GetComponent<Ball>().isReseting = false;
            var rig = ball.GetComponent<Rigidbody2D>();
            rig.isKinematic = false;
            rig.gravityScale = 1;

            Physics2D.IgnoreLayerCollision(9, 9, false);

            ball.GetComponent<Collider2D>().sharedMaterial = NoBounce;
            Debug.Log(ball.name);
            OnBallReset();
        });
    }
}
