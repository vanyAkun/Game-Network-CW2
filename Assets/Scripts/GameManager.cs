using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
public static GameManager instance;
public PlayerData playerData;
public string filePath;

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
    {

    }
    public void LoadPlayerData()
    {

    }
}
