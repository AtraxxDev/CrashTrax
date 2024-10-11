using UnityEngine;

public class HumanPlayerController : PlayerController
{

    private void Start()
    {
        InputManager.Instance.SetPlayerController(this);
    }

    private void FixedUpdate()
    {
        Move();
    }

    public override void Move()
    {
        Vector3 moveInput = InputManager.Instance.MoveInput;

        Vector3 direction = new Vector3(moveInput.x, 0, 0);

        Vector3 newPosition = rb.position + direction * Speed * Time.fixedDeltaTime;

        float clampedX = Mathf.Clamp(newPosition.x, -6.3f, 6.3f);

        newPosition = new Vector3(clampedX, newPosition.y, newPosition.z);

        rb.MovePosition(newPosition);
    }



}
