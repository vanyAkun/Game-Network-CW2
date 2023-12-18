using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab.ClientModels;

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
        if(playerLeaderboardEntries.Count > 0)
        {
            DestroyChildren(scoreHolder.transform);
            for (int i = 0; i < playerLeaderboardEntries.Count; i++)
            {
                GameObject newLeaaderboardItem = Instantiate(leaderboardItem, Vector3.zero, Quaternion.identity, scoreHolder.transform);
                newLeaaderboardItem.GetComponent<LeaderboardItem>().SetScores(i + 1, playerLeaderboardEntries[i].DisplayName, playerLeaderboardEntries[i].StatValue);
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
        foreach(Transform child in parent)
        {
            Destroy(child.gameObject);
        }
    }
}
