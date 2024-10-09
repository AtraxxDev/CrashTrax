using UnityEngine;

public abstract class PlayerController : MonoBehaviour
{
    public float Speed = 5f;
    public float minX;
    public float maxX;
    public int Lives = 15;


    public abstract void Move(Vector2 direction);

    public void ReduceLife()
    {
        Lives--;
        LivesManager.Instance.ReduceLife(this);
    }


}
