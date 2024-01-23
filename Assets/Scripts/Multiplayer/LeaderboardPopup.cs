using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab.ClientModels;
using UnityEngine.UI; // Ensure this is included for UI elements

public class LeaderboardPopup : MonoBehaviour
{
    public GameObject scoreHolder;
    public GameObject noScoreText;
    public GameObject leaderboardItem;

    private void OnEnable()
    {
        GameManager.instance.globalLeaderboard.GetLeaderboard();
    }

    public void UpdateUI(List<PlayerLeaderboardEntry> playerLeaderboardEntries)
    {
        if (playerLeaderboardEntries.Count > 0)
        {
            DestroyChildren(scoreHolder.transform);
            for (int i = 0; i < playerLeaderboardEntries.Count; i++)
            {
                GameObject newLeaderboardItem = Instantiate(leaderboardItem, Vector3.zero, Quaternion.identity, scoreHolder.transform);
                LeaderboardItem itemScript = newLeaderboardItem.GetComponent<LeaderboardItem>();
                int score = playerLeaderboardEntries[i].StatValue;
                string medal = ClassifyPlayer(score);
                itemScript.SetScores(i + 1, playerLeaderboardEntries[i].DisplayName, score, medal); // Updated to include medal
            }
            scoreHolder.SetActive(true);
            noScoreText.SetActive(false);
        }
        else
        {
            scoreHolder.SetActive(false);
            noScoreText.SetActive(true);
        }
    }

    void DestroyChildren(Transform parent)
    {
        foreach (Transform child in parent)
        {
            Destroy(child.gameObject);
        }
    }

    private string ClassifyPlayer(int kills)
    {
        if (kills <= 5)
            return "Bronze";
        else if (kills <= 10)
            return "Silver";
        else
            return "Gold";
    }
}