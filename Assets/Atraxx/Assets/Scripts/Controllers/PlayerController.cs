using UnityEngine;

public abstract class PlayerController : MonoBehaviour
{
    public Rigidbody rb;
    public float Speed = 5f;
    public float minX;
    public float maxX;
    public float minZ; 
    public float maxZ; 
    public int Lives = 15;


    public abstract void Move();

    public void ReduceLife()
    {
        Lives--;
        LivesManager.Instance.ReduceLife(this);
    }


}
