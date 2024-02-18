using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class BalloonDestroyer : MonoBehaviour
{
    public TextMeshProUGUI livesCounter;
    public int lifes;
    public bool gameOver;
    public GameObject GameOverPanel;
    public TextMeshProUGUI Score;
    int currentScore;
    private BallonSpawner ballonSpawner;
    //Singleton reference for Game Over variable
    private static BalloonDestroyer _instance;

    public static BalloonDestroyer Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<BalloonDestroyer>();
                if (_instance == null)
                {
                    Debug.LogError("BalloonDestroyer instance not found in the scene.");
                }
            }
            return _instance;
        }
    }

    private void Awake()
    {
        ballonSpawner = BallonSpawner.Instance;
    }

    private void Start()
    {
        lifes = 5;
        gameOver = false;
        GameOverPanel.SetActive(false);
        currentScore = 0;
        UpdateLifeCounter();
        
    }
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            //Debug.Log("Player Destroyed");
            lifes--;
            if(lifes >= 0 )
            {
                UpdateLifeCounter();
            }
            if(lifes < 0 )
            {
                OnGameOver();
            }
            Destroy(other.gameObject);
        }
    }

    private void UpdateLifeCounter()
    {
        livesCounter.text = lifes.ToString();
    }

    private void OnGameOver()
    {
        //Debug.Log("Game over");
        gameOver = true;
        GameOverPanel.SetActive(true);
        currentScore = ballonSpawner.finalScore;
        Score.text = currentScore.ToString();
    }

}
