using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using System.IO;
using UnityEditor.Experimental.GraphView;

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
    public ParticleSystem popEffect;
    void Start()
    {

        //ballonForce = 35f;
        //spawnInterval = 1f;

        //string jsonFilePath = Path.Combine(Application.streamingAssetsPath, "unity_data.json");
        //jsonFilePath = jsonFilePath.Replace("\\", "/");
        //Debug.Log("JSON File Path: " + jsonFilePath);
        TextAsset jsonFile = Resources.Load<TextAsset>("unity_data");
        //if (File.Exists(jsonFilePath))
        if (jsonFile != null)
        {
            //Debug.Log("Python integrated");
            //string jsonText = File.ReadAllText(jsonFile);
            string jsonText = jsonFile.text;
            // Parse the JSON data
            var jsonData = JsonUtility.FromJson<SpawnData>(jsonText);
            /*
                It is using Unity's Jsonutility class to convert JSON formatted string('jsontext') into a C# object of type SpawnData.
                1)Jsonutilityis a class provided by unity for working with JSON data.
                2)FromJson is a method to convert JSON string to an object of type 'SpawnData'
             */
            //var jsonData = JsonUtility.FromJson<SpawnData>(jsonText);

            // Update the spawn interval and balloon force
            spawnInterval = jsonData.spawnInterval;
            ballonForce = jsonData.balloonForce;

            Debug.Log("JSON Data Loaded: " + jsonText);
        }
        else
        {
            Debug.LogError("Failed to load JSON file");
        }
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
               
                popEffect.transform.position = hit.collider.transform.position;
                popEffect.Play();
                
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
