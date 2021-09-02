using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public Text Score;
    void Update()
    {
        Score.text = "RedTeam  " + GameObject.Find("Ball").GetComponent<Ball>().RedScore + "  :  " + GameObject.Find("Ball").GetComponent<Ball>().BlueScore + "  BlueTeam";
    }
}
