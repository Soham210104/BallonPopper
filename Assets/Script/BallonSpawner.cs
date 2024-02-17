using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BallonSpawner : MonoBehaviour
{
    public GameObject[] ballons;
    public Transform spawner;
    [SerializeField]private float  spawnInterval;
    [SerializeField]private float ballonForce;
    private float spawnPosChanged;
    public TextMeshProUGUI scoreText;
    public int score;
    public AudioSource audio;
    void Start()
    {
        
        ballonForce = 35f;
        spawnInterval = 1f;
        spawnPosChanged = spawnInterval;
        InvokeRepeating("balloonSpawner", 2f, spawnInterval);
        InvokeRepeating("SpawnerPositionChanged", 2f, spawnPosChanged);
        score = 0;
        UpdateScoreText();
    }

    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            HandleMouseClick();
        }
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
    void HandleMouseClick()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // Cast a ray from the mouse position
        if (Physics.Raycast(ray, out hit))
        {
            // Check if the clicked object has the "Player" tag
            if (hit.collider.CompareTag("Player"))
            {
                audio.Play();
                Destroy(hit.collider.gameObject);

                score++;
                UpdateScoreText();
            }
        }
    }

    void UpdateScoreText()
    {
         scoreText.text = "Score: " + score;
    }
}
