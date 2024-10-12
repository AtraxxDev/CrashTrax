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
        // Encuentra todos los jugadores en la escena
        players = new List<PlayerController>(FindObjectsOfType<PlayerController>());

        // Inicializa a cada jugador y los suscribe al UI Manager
        foreach (var player in players)
        {
            LivesManager.Instance.InitializeLives(player, 15);
            UIManager.Instance.SubscribeToPlayer(player);
        }

        Debug.Log("Game started with " + players.Count + " players.");
    }

    public void EndGame()
    {
        Debug.Log("El juego ha terminado.");
    }

    public void PlayerOutOfLives(PlayerController player)
    {
        player.gameObject.SetActive(false);  
        UIManager.Instance.UnsubscribeFromPlayer(player);  
        CheckGameOver();
    }

    private void CheckGameOver()
    {
        int activePlayers = 0;

        foreach (var player in players)
        {
            if (player.Lives > 0) activePlayers++;
        }

        if (activePlayers <= 1) EndGame();
    }
}
