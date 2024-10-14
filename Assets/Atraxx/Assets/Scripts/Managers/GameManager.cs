using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public List<PlayerController> players;
    public ObjectPooling objectPooling;
    public BallSpawner ballSpawner;
    public List<GameObject> goals;

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

    public void StartGame()
    {
        objectPooling.ResetPool();
        Time.timeScale = 1f;

        foreach (var player in players)
        {
            LivesManager.Instance.InitializeLives(player, 15);
            UIManager.Instance.SubscribeToPlayer(player);
            player.gameObject.SetActive(true);
        }

        objectPooling.InitializePool();
        ballSpawner.StartSpawning();
        DeactivateGoals();
    }

    public void EndGame(PlayerController winner)
    {
        Time.timeScale = 0f;
        UIManager.Instance.DisplayWinner(winner);
        UIManager.Instance.ShowGameOverPanel();
        ballSpawner.StopSpawning();

    }

    public void PlayerOutOfLives(PlayerController player)
    {
        player.gameObject.SetActive(false);
        UIManager.Instance.UnsubscribeFromPlayer(player);
        CheckGameOver();
    }

    private void CheckGameOver()
    {
        PlayerController winner = null;
        int activePlayers = 0;

        foreach (var player in players)
        {
            if (player.Lives > 0)
            {
                activePlayers++;
                winner = player;
            }
        }

        if (activePlayers <= 1)
        {
            EndGame(winner);
        }
    }

    private void DeactivateGoals()
    {
        foreach (var goal in goals)
        {
            goal.GetComponent<Goal>().indicator.SetActive(false);
        }
    }


}
