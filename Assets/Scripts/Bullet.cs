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
        Tank tank = collision.gameObject.GetComponent<Tank>();
        print(collision.collider);
        if (tank)
        {
            if (handler == tank.ID) return;
            tank.dealDamage(damage, handler);
        }     
        Destroy(gameObject);
    }
}
