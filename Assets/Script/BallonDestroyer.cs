using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallonDestroyer : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        
        GameObject otherGameObject = collision.gameObject;

        
        if (otherGameObject.CompareTag("Player"))
        {
            Debug.Log("Player Destroyed");
            Destroy(otherGameObject);
        }
    }
}
