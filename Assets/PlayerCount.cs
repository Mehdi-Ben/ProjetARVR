using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCount : MonoBehaviour
{
    public string[] textRank = {"1st", "2nd","3rd","4th","5th","6th","7th","8th"};
    public Text rank;
    public Text nbPlayer;
    public Tank player;
    public Text PV;
    public Text score;
    public Image PVGauge;

    public Image invisibility;
    public Image attackUp;
    public Image homingMissile;

    public float pvCourant;

	// Use this for initialization
	void Start ()
    {
        
    }
	
	// Update is called once per frame
	void Update ()
    {
        
        Tank[] tanks = GameObject.FindObjectsOfType<Tank>();
        if (!player)
        {
            foreach (Tank tank in tanks)
            {
                if (tank.isLocalPlayer) player = tank; break;
            }
        }
        if (!player) return;
        PVGauge.fillAmount -= (PVGauge.fillAmount - (player.PV * 0.01f)) * 0.2f;
        PV.text = (Mathf.Round(PVGauge.fillAmount*100)).ToString("00");
        PVGauge.color = Tank.colors[(player.ID - 1)];
        score.text = player.score.ToString("00");
        score.color = Tank.colors[(player.ID - 1)];
        nbPlayer.color = Tank.colors[(player.ID - 1)];

        nbPlayer.text = tanks.Length.ToString();

        int r = 0;
        foreach(Tank tank in tanks)
        {
            if (tank.score > player.score) r++;
        }
        rank.text = textRank[r];
        rank.color = Tank.colors[(player.ID - 1)];

        if (player.invisibility > 0)
        {
            invisibility.gameObject.SetActive(true);
            invisibility.fillAmount = player.invisibility / 15.0f;
        }
        else
        {
            invisibility.gameObject.SetActive(false);
        }

        if (player.attackUp > 0)
        {
            attackUp.gameObject.SetActive(true);
            attackUp.fillAmount = player.attackUp / 15.0f;
        }
        else
        {
            attackUp.gameObject.SetActive(false);
        }

        if (player.homingMissile > 0)
        {
            homingMissile.gameObject.SetActive(true);
            homingMissile.fillAmount = player.attackUp / 15.0f;
        }
        else
        {
            homingMissile.gameObject.SetActive(false);
        }

    }
}
