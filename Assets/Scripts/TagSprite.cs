using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TagSprite : MonoBehaviour {

    // Use this for initialization
    public float distance;
    public Transform target;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        //Vector3 v = target.position.normalized * distance;
        transform.position = (new Vector3(target.position.x, 0, target.position.z)).normalized * distance;
        transform.LookAt(Vector3.zero);
	}
}
