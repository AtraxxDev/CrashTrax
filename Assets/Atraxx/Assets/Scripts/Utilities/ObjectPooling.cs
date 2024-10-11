using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    public GameObject bulletPrefab; // Prefab de la pelota
    public int poolSize = 10; // Tamaño inicial del pool
    private List<GameObject> pool; // Lista para almacenar los objetos del pool

    private void Awake()
    {
        pool = new List<GameObject>();

        // Inicializar el pool de pelotas
        for (int i = 0; i < poolSize; i++)
        {
            GameObject ball = Instantiate(bulletPrefab);
            ball.SetActive(false); // Desactivar la pelota inicialmente
            pool.Add(ball); // Agregarla al pool
        }
    }

    public GameObject GetPooledBall()
    {
        // Buscar una pelota inactiva en el pool
        foreach (GameObject ball in pool)
        {
            if (!ball.activeInHierarchy)
            {
                return ball; // Retornar la pelota inactiva
            }
        }

        // Si no hay pelotas inactivas, crear una nueva
        GameObject newBall = Instantiate(bulletPrefab);
        newBall.SetActive(false); // Asegurarse de que esté inactiva al crearla
        pool.Add(newBall); // Agregar la nueva pelota al pool
        return newBall; // Retornar la nueva pelota
    }
}
