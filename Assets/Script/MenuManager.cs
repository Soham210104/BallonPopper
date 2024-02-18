using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class MenuManager : MonoBehaviour
{
    public PlayFabManager playFabManager;
    public TextMeshProUGUI Rank,ID,Score;
    public GameObject LeaderBoardPanel;
    // Start is called before the first frame update
    void Start()
    {
        playFabManager = FindObjectOfType<PlayFabManager>();
    }
    
    // Update is called once per frame


    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    
    public void LeaderBoard()
    {
        LeaderBoardPanel.SetActive(true);
        playFabManager.FetchLeaderboardData((rank, id, score) =>
        {
            Rank.text = rank.ToString();
            ID.text = id;
            Score.text = score.ToString();
        });
    }
    
    public void Close()
    {
        LeaderBoardPanel.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
