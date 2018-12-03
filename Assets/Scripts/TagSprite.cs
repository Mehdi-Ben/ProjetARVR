using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TagSprite : MonoBehaviour {

    // Use this for initialization
    public TextMesh text;
    public TextMesh shadowText;
    public SpriteRenderer sprite;
    public int playerId;
    public Color playerColor;
    
    

	void Start ()
    {
        text.text = "P" + playerId;
        shadowText.text = text.text;
        text.color = playerColor;
        sprite.color = playerColor;
    }
	
	// Update is called once per frame
	void Update ()
    {
       
	}
}
