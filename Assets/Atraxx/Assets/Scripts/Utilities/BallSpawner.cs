using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    public ObjectPooling objectPool; // Referencia al Object Pool
    public float spawnRadius = 5f; // Radio de generación
    public float spawnInterval = 2f; // Intervalo de generación
    public float initialForce = 5f; // Fuerza inicial aplicada a las pelotas

    private float timer = 0; // Temporizador

    private void Start()
    {
        objectPool = FindObjectOfType<ObjectPooling>();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnBall();
            timer = 0; // Reiniciar el temporizador
        }
    }

    public void SpawnBall()
    {
        GameObject ball = objectPool.GetPooledBall();

        if (ball != null)
        {
            // Generar una posición de spawn aleatoria dentro del radio
            Vector3 spawnPosition = transform.position + Random.insideUnitSphere * spawnRadius;
            spawnPosition.y = transform.position.y; // Asegurarse de que la altura sea la misma

            ball.transform.position = spawnPosition; // Colocar la pelota en la posición aleatoria
            ball.SetActive(true); // Activar la pelota

            // Generar una dirección aleatoria pero evitando el eje Y positivo
            Vector3 randomDirection = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;

            ball.GetComponent<Rigidbody>().AddForce(randomDirection * initialForce, ForceMode.Impulse); // Aplicar fuerza
        }
    }

    private void OnDrawGizmos()
    {
        // Establecer el color del gizmo
        Gizmos.color = Color.green;

        // Dibujar una esfera en el centro del spawner con el radio especificado
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }
}
