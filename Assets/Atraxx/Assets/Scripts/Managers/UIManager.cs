using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    // Referencia a los textos de vidas de cada jugador
    public List<TMP_Text> playerLivesTexts;  // Asigna estos en el Inspector

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

    // Método para actualizar la UI del jugador correspondiente
    public void UpdateLivesUI(PlayerController player)
    {
        int playerIndex = GameManager.Instance.players.IndexOf(player);  // Buscar el índice del jugador
        if (playerIndex >= 0 && playerIndex < playerLivesTexts.Count)
        {
            playerLivesTexts[playerIndex].text = $"Lives: {player.Lives}";
        }
    }

    // Suscribimos al UI Manager para escuchar eventos de cambios de vidas
    public void SubscribeToPlayer(PlayerController player)
    {
        player.OnLivesChanged += UpdateLivesUI;
    }

    // Desuscribimos al UI Manager cuando ya no es necesario
    public void UnsubscribeFromPlayer(PlayerController player)
    {
        player.OnLivesChanged -= UpdateLivesUI;
    }
}
