using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class RankingScript : NetworkBehaviour
{
    public PlayerRanking[] rankings;
    public Camera cam;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update ()
    {
        print("Client : " + isClient + " Server : " + isServer);
        if (isClient) Destroy(gameObject);
        foreach (PlayerRanking pr in rankings)
        {
            pr.gameObject.SetActive(false);
        }
        cam.transform.position = new Vector3(55 * Mathf.Cos(Time.time * 0.2f), 35, 55 * Mathf.Sin(Time.time * 0.2f));
        cam.transform.LookAt(Vector3.zero);

        Tank[] players = FindObjectsOfType<Tank>();
        for (int i = 0; i < players.Length; i++)
        {
            Tank t = players[i];
            rankings[t.ID-1].gameObject.SetActive(true);
            rankings[t.ID-1].tank = t;
            rankings[t.ID - 1].text.text = "P" + t.ID;
            rankings[t.ID - 1].score.text = t.score.ToString("00");
            rankings[t.ID - 1].img1.color = Tank.colors[t.ID - 1];
            rankings[t.ID - 1].img2.color = Tank.colors[t.ID - 1];
        }

    }
}
