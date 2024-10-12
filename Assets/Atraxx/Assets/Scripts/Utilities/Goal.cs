using UnityEngine;
using System.Collections;

public class Goal : MonoBehaviour
{
    public PlayerController player;  
    public LayerMask ballLayer;
    private bool goalScored = false;

    private void OnTriggerEnter(Collider other)
    {
        if (goalScored) return;  

        Debug.Log($"Colisión con: {other.gameObject.name}");

        if (((1 << other.gameObject.layer) & ballLayer) != 0)
        {
            goalScored = true;


            player.ReduceLife();


            other.gameObject.SetActive(false);


            goalScored = false;
        }
    }


}
