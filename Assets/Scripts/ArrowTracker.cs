﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTracker : MonoBehaviour
{
    public Vector3 delta;
    public float speed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.position = transform.parent.position + delta;
        transform.Rotate(Vector3.up, speed * Time.deltaTime);
	}
}
