using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallonSpawner : MonoBehaviour
{
    public GameObject[] ballons;
    public Transform spawner;
    // Start is called before the first frame update
    void Start()
    {
        spawner = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        balloonSpawner();
    }

    IEnumerator balloonSpawner()
    {
        GameObject randomBalloon = ballons[Random.Range(0, ballons.Length)];
        Instantiate(randomBalloon, spawner.position, Quaternion.identity);
        yield return new WaitForSeconds(2f);
    }
}
