using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    public ObjectPooling objectPool;
    public float spawnRadius = 5f;
    public float spawnInterval = 2f;
    public float initialForce = 5f;

    private float timer = 0;
    private bool isSpawning = false;  

    private void Start()
    {
        objectPool = FindObjectOfType<ObjectPooling>();
    }

    private void Update()
    {
        if (!isSpawning) return;  

        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnBall();
            timer = 0;
        }
    }

    public void SpawnBall()
    {
        GameObject ball = objectPool.GetPooledBall();

        if (ball != null)
        {
            Vector3 spawnPosition = transform.position + Random.insideUnitSphere * spawnRadius;
            spawnPosition.y = transform.position.y;

            ball.transform.position = spawnPosition;
            ball.SetActive(true);

            Vector3 randomDirection = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
            ball.GetComponent<Rigidbody>().AddForce(randomDirection * initialForce, ForceMode.Impulse);
        }
    }

    public void StartSpawning() => isSpawning = true; 

    public void StopSpawning() => isSpawning = false;  

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }
}
