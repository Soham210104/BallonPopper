using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using System.IO;
using UnityEditor.Experimental.GraphView;
using UnityEngine.SceneManagement;
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
    private BalloonDestroyer balloonDestroyer;

    public PlayFabManager playFab;
    public TextMeshProUGUI displayHighScore;
    public int highestScore;
    public int finalScore;
    private static BallonSpawner instance;
   
    public static BallonSpawner Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<BallonSpawner>();

                if (instance == null)
                {
                    Debug.LogError("BalloonDestroyer instance not found in the scene.");
                }
            }
            return instance;
        }
    }

    // Assign the BalloonDestroyer.Instance in Awake or Start
    private void Awake()
    {
        playFab = FindObjectOfType<PlayFabManager>();
        balloonDestroyer = BalloonDestroyer.Instance;
    }
    void Start()
    {
        
        //ballonForce = 35f;
        //spawnInterval = 1f;

//----------------------THIS WAS THE FIRST APPROACH USED TO GET ACCESS TO THE UNITY-JSON SCRIPT but it wont worked---------------------------------
        //string jsonFilePath = Path.Combine(Application.streamingAssetsPath, "unity_data.json");
        //jsonFilePath = jsonFilePath.Replace("\\", "/");
        //Debug.Log("JSON File Path: " + jsonFilePath);
//---------------------------------------------------------------------------------------------------------

//-----------------------This is the another approoach i have used------------------------
        //Resources is a special class which allows to load assets at a runtime from folder named resource
        //Asset from this folder can be used with the help of Resource.Load.
        //TextAsset class is used to represent a text asset in Unity  such as text file,here unity-data is the text file.
        TextAsset jsonFile = Resources.Load<TextAsset>("unity_data");

        //if (File.Exists(jsonFilePath))


        if (jsonFile != null)
        {
         
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
        if(balloonDestroyer.gameOver==true)
        {
            CancelInvoke("balloonSpawner");
            CancelInvoke("SpawnerPositionChanged");
        }

        if (Input.GetMouseButtonDown(0) && balloonDestroyer.gameOver == false)
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
         finalScore = score;

        if (finalScore >= highestScore)
        {
            highestScore = finalScore;      
        }
        displayHighScore.text = highestScore.ToString();

        
    }

    public void SendDataToLeaderBoard()
    {
        playFab.SendLeaderBoard(finalScore);
    }

    public void MenuScene()
    {
        SceneManager.LoadScene(0);
    }
    
}
