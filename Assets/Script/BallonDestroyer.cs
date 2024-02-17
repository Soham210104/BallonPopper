using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class BalloonDestroyer : MonoBehaviour
{
    public TextMeshProUGUI livesCounter;
    public int lifes;

    private void Start()
    {
        lifes = 5;
        UpdateLifeCounter();
    }
    private void OnTriggerEnter(Collider other)
    {
        // Check if the triggering object has the "Player" tag
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player Destroyed");
            lifes--;
            if(lifes >= 0 )
            {
                UpdateLifeCounter();
            }
            Destroy(other.gameObject);
        }
    }

    private void UpdateLifeCounter()
    {
        livesCounter.text = lifes.ToString();
    }

   
}
