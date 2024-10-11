using UnityEngine;

public class LivesManager : MonoBehaviour
{
    public static LivesManager Instance { get; private set; }

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

    public void InitializeLives(PlayerController player, int startingLives)
    {
        player.Lives = startingLives;
    }

    public void ReduceLife(PlayerController player)
    {
        if (player.Lives >= 0)
        {
            GameManager.Instance.PlayerOutOfLives(player);
        }
    }

}
