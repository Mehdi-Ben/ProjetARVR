using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerRotation : MonoBehaviour
{
    public float speed;
    public float timer;
    public TextMesh[] texts;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(Vector3.up, speed * Time.deltaTime);
        foreach (TextMesh text in texts)
        {
            text.text = ((int)(timer / 60)).ToString("00") + ":" + ((int)(timer % 60)).ToString("00");
        }
        timer -= Time.deltaTime;
    }
}
