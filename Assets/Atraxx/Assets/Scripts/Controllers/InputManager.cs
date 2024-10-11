using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }
    public PlayerController PlayerController;

    private PlayerMoves controls;

    public Vector2 MoveInput { get; private set; }



    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            controls = new PlayerMoves();
            InitializeControls();
            controls.Enable();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnDisable()
    {
        controls.Disable();
    }



    public void InitializeControls()
    {
        // Movimiento
        controls.Player.Move.performed += ctx => MoveInput = ctx.ReadValue<Vector2>();
        controls.Player.Move.canceled += ctx => MoveInput = Vector2.zero;
    }

    public void SetPlayerController(PlayerController controller)
    {
        PlayerController = controller;
        
    }





}
