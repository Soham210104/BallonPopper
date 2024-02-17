using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonDestroyer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Check if the triggering object has the "Player" tag
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player Destroyed");
            Destroy(other.gameObject);
        }
    }
}
