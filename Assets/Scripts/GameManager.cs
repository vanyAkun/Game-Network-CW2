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
    public void SavePlayerData() //JSON 64 CODE DECODE
    {
        string serialisedDataString = JSON.Serialize(playerData).CreateString();
        // Encode the JSON string to Base64
        byte[] bytesToEncode = System.Text.Encoding.UTF8.GetBytes(serialisedDataString);
        string encodedText = Convert.ToBase64String(bytesToEncode);
        File.WriteAllText(filePath, encodedText);
    }
    public void LoadPlayerData()
    {
        if (!File.Exists(filePath))
        {
            playerData = new PlayerData();
            SavePlayerData();
        }
        else
        {
            string encodedText = File.ReadAllText(filePath);
            // Decode the Base64 string back to JSON string
            byte[] decodedBytes = Convert.FromBase64String(encodedText);
            string decodedJsonString = System.Text.Encoding.UTF8.GetString(decodedBytes);
            playerData = JSON.ParseString(decodedJsonString).Deserialize<PlayerData>();
        }
    }
}
