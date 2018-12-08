using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class AutoGuideBullet : NetworkBehaviour
{
    public float power;
    public Vector3 vect;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        vect = Vector3.zero;
        foreach (Tank tank in FindObjectsOfType<Tank>() )
        {
            if (tank.ID != GetComponent<Bullet>().handler)
            {
                Vector3 d = (tank.gameObject.transform.position - transform.position);
                vect += d.normalized * (1f / d.magnitude);
            }
        }
        vect = vect * power;
        GetComponent<Bullet>().direction = (GetComponent<Bullet>().direction + vect)*0.5f;

    }
}
