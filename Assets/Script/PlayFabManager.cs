using PlayFab.ClientModels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;

public class PlayFabManager : MonoBehaviour
{
    //public BallonSpawner spawner;
    int SCORE;
    // Start is called before the first frame update

    private void Awake()
    {
        //spawner = FindObjectOfType<BallonSpawner>();
    }
    void Start()
    {
       
        Login();
    }
  
    // Update is called once per frame
    void Login()
    {
        var request = new LoginWithCustomIDRequest
        {
            CustomId = SystemInfo.deviceUniqueIdentifier,
            CreateAccount = true
        };
        PlayFabClientAPI.LoginWithCustomID(request, OnSuccess, OnError);
    }

    void OnSuccess(LoginResult result)
    {
        Debug.Log("Succesful Login");
    }

    void OnError(PlayFabError error)
    {
        Debug.Log("Error while logging in");
        Debug.Log(error.GenerateErrorReport());
    }

    public void SendLeaderBoard(int score)
    {
        // Get the current score from the BallonSpawner
        Debug.Log("Sending leaderboard...");
        
        Debug.Log("Current Score: " + score);
        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>
        {
            new StatisticUpdate
            {
                StatisticName = "GameScore",
                Value = score
            }
        }
        };

        PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderBoardUpdate, OnError);
    }

    void OnLeaderBoardUpdate(UpdatePlayerStatisticsResult result)
    {
        Debug.Log("Succesfull leaderboard sent");
    }

}
