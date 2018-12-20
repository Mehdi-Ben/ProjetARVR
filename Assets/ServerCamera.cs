using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ServerCamera : NetworkBehaviour {

    public float speed;
	
	// Update is called once per frame
	void Update ()
    {
        if (!isServer) Destroy(this);
        transform.position = new Vector3(30 * Mathf.Cos(Time.time * speed), 60, 30 * Mathf.Sin(Time.time * speed));
        print("Ok");
        transform.LookAt(Vector3.zero);
	}
}
