using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    public List<TMP_Text> playerLivesTexts;
    public GameObject startPanel;
    public GameObject gameOverPanel;
    public Button startButton;
    public Button restartButton;
    public TMP_Text winnerText;

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
        startButton.onClick.AddListener(StartGame);
        restartButton.onClick.AddListener(RestartGame);
        ShowStartPanel();
        HideGameOverPanel();
    }

    public void UpdateLivesUI(PlayerController player)
    {
        int playerIndex = GameManager.Instance.players.IndexOf(player);
        if (playerIndex >= 0 && playerIndex < playerLivesTexts.Count)
        {
            playerLivesTexts[playerIndex].text = $"Lives: {player.Lives}";
        }
    }

    public void SubscribeToPlayer(PlayerController player)
    {
        player.OnLivesChanged += UpdateLivesUI;
    }

    public void UnsubscribeFromPlayer(PlayerController player)
    {
        player.OnLivesChanged -= UpdateLivesUI;
    }

    public void ShowStartPanel() => startPanel.SetActive(true);
    public void HideStartPanel() => startPanel.SetActive(false);
    public void ShowGameOverPanel() => gameOverPanel.SetActive(true);
    public void HideGameOverPanel() => gameOverPanel.SetActive(false);

    public void DisplayWinner(PlayerController winner)
    {
        winnerText.text = $"{winner.name} ¡Ha ganado!";
    }

    private void StartGame()
    {
        HideStartPanel();
        GameManager.Instance.StartGame();
        UpdateAllLivesUI();
    }

    private void RestartGame()
    {
        HideGameOverPanel();
        GameManager.Instance.StartGame();
        UpdateAllLivesUI();
    }

    private void UpdateAllLivesUI()
    {
        foreach (var player in GameManager.Instance.players)
        {
            UpdateLivesUI(player);
        }
    }
}
