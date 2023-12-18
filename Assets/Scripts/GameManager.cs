using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Leguar.TotalJSON;
using PlayFab;
using PlayFab.ClientModels;
using System;

public class GameManager : MonoBehaviour
{
public static GameManager instance;
public PlayerData playerData;
public string filePath;
public GlobalLeaderboard globalLeaderboard;
    private void Start()
    {
        LoadPlayerData();
        LoginToPlayFab();

    }
    void LoginToPlayFab()
    {
        LoginWithCustomIDRequest request = new LoginWithCustomIDRequest()
        {
            CreateAccount = true,
            CustomId = playerData.uid,
        };
        PlayFabClientAPI.LoginWithCustomID(request,PlayFabLoginResult, PlayFabLoginError);
    }

    void PlayFabLoginResult (LoginResult loginResult)
    {
        Debug.Log("PlayFab = Login Succeeded: " +  loginResult.ToJson());
    }

    void PlayFabLoginError (PlayFabError loginError)
    {
        Debug.Log("PlayFab - Login Failed: " + loginError.ErrorMessage);
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    public void SavePlayerData()
    {  string serialisedDataString = JSON.Serialize(playerData).CreateString();
        File.WriteAllText(filePath, serialisedDataString);
    }
    public void LoadPlayerData()
    {
        if (!File.Exists(filePath))
        {
            playerData = new PlayerData();
            SavePlayerData();
        }
        string fileContents = File.ReadAllText(filePath);
        playerData = JSON.ParseString(fileContents).Deserialize<PlayerData>();
    }
}
