using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TagSprite : MonoBehaviour {

    // Use this for initialization
    public TextMesh text;
    public int playerId;
    public Color playerColor;
    
    

	void Start ()
    {
        text.text = "P" + playerId;
        text.color = playerColor;
	}
	
	// Update is called once per frame
	void Update ()
    {
       
	}
}
