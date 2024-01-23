using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardItem : MonoBehaviour
{
    public Text order;
    public Text username;
    public Text score;
    public Text medalText;

    public void SetScores(int _order, string _username, int _score, string _medal)
    {
        order.text = _order.ToString() + ")";
        username.text = _username;
        score.text = _score.ToString();
        medalText.text = _medal; 
    }
}
