using UnityEngine;

public class HumanPlayerController : PlayerController
{

    private void Start()
    {
        InputManager.Instance.SetPlayerController(this);
    }

    public override void Move(Vector2 direction)
    {
        Debug.Log($"Move direction: {direction}");

        // Calcula la nueva posici�n en X sin limitaciones
        float newX = transform.position.x + direction.x * Speed * Time.deltaTime;

        // Actualiza la posici�n del jugador
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }

}
