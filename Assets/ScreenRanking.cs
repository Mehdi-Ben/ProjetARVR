using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenRanking : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        GetComponent<Text>().text = "SCORE\n";
        Tank[] players = FindObjectsOfType<Tank>();
        for (int i = 0; i < players.Length; i++)
        {
            Tank t = players[i];
            GetComponent<Text>().text += "P" + t.ID+"      "+ t.score.ToString("00")+ "\n";
        }
    }
}
