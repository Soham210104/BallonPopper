using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallonSpawner : MonoBehaviour
{
    public GameObject[] ballons;
    public Transform spawner;
    [SerializeField]private float  spawnInterval;
    [SerializeField]private float ballonForce;
    private float spawnPosChanged;
    // Start is called before the first frame update
    void Start()
    {
        //spawner = GetComponent<Transform>();
        ballonForce = 35f;
        spawnInterval = 1f;
        spawnPosChanged = spawnInterval;
        InvokeRepeating("balloonSpawner", 2f, spawnInterval);
        InvokeRepeating("SpawnerPositionChanged", 2f, spawnPosChanged);
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

    void SpawnerPositionChanged()
    {
        float newX = Random.Range(-2.5f, 2.5f);
        spawner.position = new Vector3(newX, spawner.position.y, spawner.position.z);
    }
}
