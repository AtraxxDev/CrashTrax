using UnityEngine;

public class AIPlayerController : PlayerController
{
    public enum MovementAxis
    {
        X,
        Z
    }

    public MovementAxis movementAxis; // Eje de movimiento seleccionado
    public float moveSpeed = 5f; // Velocidad de movimiento de la IA
    public float stoppingDistance = 0.5f; // Distancia para detenerse cerca de la pelota

    private void FixedUpdate()
    {
        Move();
    }

    public override void Move()
    {
        // Buscar todas las pelotas en la escena
        Ball[] balls = FindObjectsOfType<Ball>();

        // Inicializar variables para la pelota más cercana
        Ball closestBall = null;
        float closestDistance = Mathf.Infinity;

        // Iterar a través de todas las pelotas para encontrar la más cercana
        foreach (Ball ball in balls)
        {
            if (ball != null) // Verifica que la pelota no sea nula
            {
                float distance = Vector3.Distance(transform.position, ball.transform.position);

                // Si la distancia es menor que la más cercana encontrada hasta ahora, actualiza
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestBall = ball;
                }
            }
        }

        // Si se encontró una pelota cercana
        if (closestBall != null)
        {
            // Calcular la dirección hacia la pelota más cercana
            Vector3 direction = (closestBall.transform.position - transform.position).normalized;

            // Verificar la distancia para detenerse
            if (closestDistance > stoppingDistance)
            {
                // Calcular la nueva posición según el eje de movimiento
                Vector3 newPosition;

                if (movementAxis == MovementAxis.X)
                {
                    newPosition = rb.position + new Vector3(direction.x, 0, 0) * moveSpeed * Time.fixedDeltaTime;
                    // Clampeo del movimiento en el eje X
                    float clampedX = Mathf.Clamp(newPosition.x, minX, maxX);
                    newPosition = new Vector3(clampedX, newPosition.y, newPosition.z);
                }
                else // Movimiento en el eje Z
                {
                    newPosition = rb.position + new Vector3(0, 0, direction.z) * moveSpeed * Time.fixedDeltaTime;
                    // Clampeo del movimiento en el eje Z
                    float clampedZ = Mathf.Clamp(newPosition.z, minZ, maxZ);
                    newPosition = new Vector3(newPosition.x, newPosition.y, clampedZ);
                }

                // Mover la IA usando Rigidbody
                rb.MovePosition(newPosition);
            }
        }
    }
}
