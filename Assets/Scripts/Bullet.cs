using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Bullet : NetworkBehaviour {
    [SyncVar] public float speed;
    [SyncVar] public Vector3 direction;
    [SyncVar] public int handler;
    [SyncVar] public float damage;

    private void Start()
    {
        Destroy(gameObject, 5f);
        if (handler > 0) GetComponent<MeshRenderer>().material.color = Tank.colors[handler - 1];
    }
    private void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Tank>() && collision.gameObject.GetComponent<Tank>().ID == handler) return;
        if (collision.gameObject.GetComponent<Tank>())
        {
            Tank tank = collision.gameObject.GetComponent<Tank>();
            if(tank != null)
            {
                tank.dealDamage(damage, handler);
            }
        }
        Destroy(gameObject);
    }
}
