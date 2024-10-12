using UnityEngine;

public abstract class PlayerController : MonoBehaviour
{
    public Rigidbody rb;
    public float Speed;
    public float minX;
    public float maxX;
    public float minZ; 
    public float maxZ; 
    public int Lives = 15;

    public delegate void LivesChanged(PlayerController player);
    public event LivesChanged OnLivesChanged;

    public abstract void Move();

    public void ReduceLife()
    {
        Lives--;

        OnLivesChanged?.Invoke(this);
        if (Lives <= 0)
        {
            GameManager.Instance.PlayerOutOfLives(this);
        }
    }


}
