using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerRanking : MonoBehaviour
{
    public Tank tank;

    public Text text;
    public Text score;
    public Image img1;
    public Image img2;



	
	// Update is called once per frame
	void Update ()
    {
        if (!tank) return;
        text.text = "P" + tank.ID;
        score.text = tank.score.ToString("00");
        img1.color = Tank.colors[tank.ID - 1];
        img2.color = Tank.colors[tank.ID - 1];
        print("Score : " + tank.score);
    }

    private void OnDrawGizmos()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        Gizmos.color = Color.red;
        for (int i = 0; i < players.Length; i++)
        {
            Tank t = players[i].GetComponent<Tank>();
            Gizmos.DrawSphere(t.transform.position + new Vector3(0, 7, 0), 3*(t.PV / 100));
            Gizmos.DrawWireSphere(t.transform.position + new Vector3(0, 7, 0), 3);
        }
    }
}
