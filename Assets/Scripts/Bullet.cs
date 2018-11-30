using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Bullet : NetworkBehaviour {

    [SyncVar] public int handler;
    [SyncVar] public float damage;

    private void Start()
    {
        Destroy(gameObject, 5f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Tank>() && collision.gameObject.GetComponent<Tank>().ID == handler) return;
        if (collision.gameObject.GetComponent<Tank>())
        {
            Tank tank = collision.gameObject.GetComponent<Tank>();
            if(tank != null)
            {
                tank.dealDamage(damage);
            }
        }
        Destroy(gameObject);
    }
}
