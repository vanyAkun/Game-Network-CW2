using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

public class GlobalLeaderboard : MonoBehaviour
{
    int maxResults = 5;
    public void SubmitScore(int playerScore)
    {
        UpdatePlayerStatisticsRequest request = new UpdatePlayerStatisticsRequest()
        {
            Statistics = new List<StatisticUpdate>
            {
                new StatisticUpdate()
                {
                    StatisticName = "Most Kills",
                    Value = playerScore,
                }
            }
        };
           PlayFabClientAPI.UpdatePlayerStatistics(request, PlayFabUpdateStatsResult, PlayFabUpdateStatsError);
    }
    void PlayFabUpdateStatsResult(UpdatePlayerStatisticsResult updattePlayerStatiticsResult)
    {
        Debug.Log("PlayFab - Score submitted.");
    }
    void PlayFabUpdateStatsError (PlayFabError updatePlayerStatisticsError)
    {
        Debug.Log("PlayFab - Error ocurred while submitting score: " + updatePlayerStatisticsError.ErrorMessage);
    }
    public void GetLeaderboard()
    {
        GetLeaderboardRequest request = new GetLeaderboardRequest()
        {
            MaxResultsCount = maxResults,
            StatisticName = "Most Kills",
        };
       PlayFabClientAPI.GetLeaderboard(request,PlayFabGetLeaderboardResult, PlayFabGetLeaderboardError);
       
    }
    void PlayFabGetLeaderboardResult(GetLeaderboardResult getLeaderboardResult)
    {
        Debug.Log("PlayFab - Get Leaderboard completed");
    }

    void PlayFabGetLeaderboardError(PlayFabError getLeaderboardError)
    {
        Debug.Log("PlayFab - Error occurred while getting Leaderboard " + getLeaderboardError.ErrorMessage);
    }
}
