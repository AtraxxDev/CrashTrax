using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float bounceForce = 5f;
    public float randomnessFactor = 0.5f;

    public LayerMask interactable;

    private void OnCollisionEnter(Collision collision)
    {
        Vector3 normal = collision.contacts[0].normal;

        Vector3 bounceDirection = new Vector3(normal.x, 0, normal.z).normalized;

        Vector3 randomOffset = new Vector3(Random.Range(-randomnessFactor, randomnessFactor), 0, Random.Range(-randomnessFactor, randomnessFactor));
        bounceDirection += randomOffset;

        bounceDirection.Normalize();

        GetComponent<Rigidbody>().AddForce(bounceDirection * bounceForce, ForceMode.Impulse);
    }

}

