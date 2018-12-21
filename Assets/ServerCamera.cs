using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ServerCamera : NetworkBehaviour {

    public float speed;
	
	// Update is called once per frame
	void Update ()
    {
        if (isClient) Destroy(this);
        transform.position = new Vector3(55 * Mathf.Cos(Time.time * speed), 35, 55 * Mathf.Sin(Time.time * speed));
        transform.LookAt(Vector3.zero);
	}
}
