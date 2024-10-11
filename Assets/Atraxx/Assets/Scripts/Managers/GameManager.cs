using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public List<PlayerController> players;
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

    private void Start()
    {
        StartGame();
    }

    public void StartGame()
    {
        // Encuentra automáticamente todos los jugadores en la escena
        players = new List<PlayerController>(FindObjectsOfType<PlayerController>());

        // Inicializa cada jugador (ejemplo: establecer las vidas o cualquier otro parámetro)
        foreach (var player in players)
        {
            LivesManager.Instance.InitializeLives(player, 15);  
        }

        Debug.Log("Game started with " + players.Count + " players.");
    }

    public void EndGame()
    {
        Debug.Log("El juego ha terminado");
    }

    public void CheckGameOver()
    {
        int activePlayers = 0;

        // Cuenta cuántos jugadores aún tienen vidas
        foreach (var player in players)
        {
            if (player.Lives > 0)
            {
                activePlayers++;
            }
        }

        // Si solo queda un jugador con vidas, el juego termina
        if (activePlayers <= 1)
        {
            EndGame();
        }

    }

    public void PlayerOutOfLives(PlayerController player)
    {
        player.gameObject.SetActive(false);  // Desactiva al jugador o IA que ya no tiene vidas
        CheckGameOver();  // Verifica si el juego debe terminar
    }
}
