using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour
{
    public PhysicsMaterial2D Bounce;
    public PhysicsMaterial2D NoBounce;
    public Transform LeftBottom;
    public Transform LeftTop;
    public Transform LeftEnd;
    public Transform RightBottom;
    public Transform RightTop;
    public Transform RightEnd;
    Vector3 startPos;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.GetChild(0).position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            StartCoroutine(ShootBalls());
        }
    }

    IEnumerator ShootBalls()
    {
        foreach (Transform ball in transform)
        {
            if (ball.GetComponent<Ball>().isRunning)
                yield break;
        }

        foreach (Transform ball in transform)
        {
            ball.GetComponent<Rigidbody2D>().sharedMaterial = Bounce;
            ball.GetComponent<Ball>().Shoot(startPos);
            yield return new WaitForSeconds(0.5f);
        }
    }

    public void OnBallReset()
    {
        if (IsAllReset()) {
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
        if (isLeft){
            seq.append(LeanTween.move(ball, LeftBottom, 0.3f));
            seq.append(LeanTween.move(ball, LeftTop, 0.6f));
            seq.append(LeanTween.move(ball, LeftEnd, 0.1f));
        }
        else{
            seq.append(LeanTween.move(ball, RightBottom, 0.3f));
            seq.append(LeanTween.move(ball, RightTop, 0.6f));
            seq.append(LeanTween.move(ball, RightEnd, 0.1f));
        }

        seq.append(()=>{
            ball.GetComponent<Ball>().isRunning = false;
            ball.GetComponent<Rigidbody2D>().isKinematic = false;
            ball.GetComponent<Rigidbody2D>().sharedMaterial = NoBounce;
        });
    }
}
