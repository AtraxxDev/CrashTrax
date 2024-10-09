using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }
    public PlayerController PlayerController;



    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetPlayerController(PlayerController controller)
    {
        PlayerController = controller;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (PlayerController != null)
        {
            Vector2 moveInput = context.ReadValue<Vector2>();
            PlayerController.Move(moveInput);
        }
    }



}
