using PlayFab.ClientModels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using System;

public class PlayFabManager : MonoBehaviour
{
    //public BallonSpawner spawner;
    
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
    public void Login()
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

    public void FetchLeaderboardData(Action<int, string, int> onLeaderboardFetched)
    {
        // Fetch leaderboard data
        PlayFabClientAPI.GetLeaderboard(new GetLeaderboardRequest
        {
            StatisticName = "GameScore",
            StartPosition = 0, // Adjust as needed
            MaxResultsCount = 1 // Fetch only the player's entry
        }, (result) => OnLeaderboardGet(result, onLeaderboardFetched), OnError);
    }

    private void OnLeaderboardGet(GetLeaderboardResult result, Action<int, string, int> onLeaderboardFetched)
    {
        // Process the leaderboard data
        if (result.Leaderboard != null && result.Leaderboard.Count > 0)
        {
            var playerEntry = result.Leaderboard[0];

            // Extract relevant information
            int rank = playerEntry.Position;
            string id = playerEntry.PlayFabId;
            int score = playerEntry.StatValue;

            // Execute the provided callback with the fetched data
            onLeaderboardFetched?.Invoke(rank, id, score);
        }
        else
        {
            Debug.LogError("No leaderboard data found in the result");
        }
    }

}
