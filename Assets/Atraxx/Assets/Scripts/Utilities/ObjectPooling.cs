using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    public GameObject bulletPrefab;
    public int poolSize = 10;
    private List<GameObject> pool;

    private void Awake()
    {
        pool = new List<GameObject>();
    }

    public void InitializePool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject ball = Instantiate(bulletPrefab);
            ball.SetActive(false);
            pool.Add(ball);
        }
    }

    public GameObject GetPooledBall()
    {
        foreach (GameObject ball in pool)
        {
            if (!ball.activeInHierarchy)
            {
                ResetBallState(ball); 
                return ball;
            }
        }

        GameObject newBall = Instantiate(bulletPrefab);
        newBall.SetActive(false);
        pool.Add(newBall);
        ResetBallState(newBall);
        return newBall;
    }

    public void ResetPool()
    {
        foreach (GameObject ball in pool)
        {
            ball.SetActive(false);
            ball.transform.position = Vector3.zero;
            ResetBallState(ball); 
        }
    }

    private void ResetBallState(GameObject ball)
    {
        Rigidbody rb = ball.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }
}
