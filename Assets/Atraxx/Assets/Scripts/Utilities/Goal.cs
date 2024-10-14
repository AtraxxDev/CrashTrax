using UnityEngine;
using System.Collections;

public class Goal : MonoBehaviour
{
    public PlayerController player;  
    public LayerMask ballLayer;
    private bool goalScored = false;

    public GameObject indicator;

    private void Start()
    {
        if (indicator != null)
        {
            indicator.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (goalScored) return;  


        if (((1 << other.gameObject.layer) & ballLayer) != 0)
        {
            goalScored = true;


            player.ReduceLife();

            if (player.Lives <= 0)
            {
                ActivateIndicator();
            }


            other.gameObject.SetActive(false);


            goalScored = false;
        }
    }

    private void ActivateIndicator()
    {
        if (indicator != null)
        {
            indicator.SetActive(true);
        }
    }


}
