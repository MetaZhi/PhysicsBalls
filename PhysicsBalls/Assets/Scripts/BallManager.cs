using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour
{
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
}
