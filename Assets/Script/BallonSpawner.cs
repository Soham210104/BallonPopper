using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallonSpawner : MonoBehaviour
{
    public GameObject[] ballons;
    public Transform spawner;
    [SerializeField]private float  spawnInterval;
    [SerializeField]private float ballonForce;
    // Start is called before the first frame update
    void Start()
    {
        spawner = GetComponent<Transform>();
        ballonForce = 25f;
        spawnInterval = 6f;
        InvokeRepeating("balloonSpawner", 0f, spawnInterval);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void balloonSpawner()
    {
        GameObject randomBalloon = ballons[Random.Range(0, ballons.Length)];
        GameObject newBallon = Instantiate(randomBalloon, spawner.position, randomBalloon.transform.rotation);
        Rigidbody ballonRigidBody = newBallon.GetComponent<Rigidbody>();
        ballonRigidBody.AddForce(Vector3.up * ballonForce, ForceMode.Acceleration);
    }
}
