using UnityEngine;

public class AIPlayerController : PlayerController
{
    public enum MovementAxis
    {
        X,
        Z
    }

    public MovementAxis movementAxis;
    public float detectionRadius = 3f; 
    public LayerMask ballLayer; 

    private Vector3 initialPosition;
    private bool isReturning = false;

    private void Start()
    {
        initialPosition = transform.position;
    }

    private void FixedUpdate()
    {
        Move();
    }

    public override void Move()
    {
        Vector3 currentPosition = rb.position;

        if (!isReturning)
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRadius, ballLayer);

            if (hitColliders.Length > 0)
            {
                Transform ball = hitColliders[0].transform;

                if (movementAxis == MovementAxis.X)
                {
                    currentPosition.x = Mathf.MoveTowards(currentPosition.x, ball.position.x, Speed * Time.fixedDeltaTime);
                }
                else 
                {
                    currentPosition.z = Mathf.MoveTowards(currentPosition.z, ball.position.z, Speed * Time.fixedDeltaTime);
                }
            }
            else
            {
                isReturning = true;
            }
        }
        else
        {
            if (movementAxis == MovementAxis.X)
            {
                currentPosition.x = Mathf.MoveTowards(currentPosition.x, initialPosition.x, Speed * Time.fixedDeltaTime);
            }
            else 
            {
                currentPosition.z = Mathf.MoveTowards(currentPosition.z, initialPosition.z, Speed * Time.fixedDeltaTime);
            }

            if (Vector3.Distance(currentPosition, initialPosition) < 0.1f)
            {
                currentPosition = initialPosition;
                isReturning = false; 
            }
        }

        rb.MovePosition(currentPosition);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
