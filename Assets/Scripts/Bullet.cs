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
        transform.position += direction.normalized * speed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Tank tank = collision.gameObject.GetComponent<Tank>();
        print("["+gameObject.GetInstanceID()+"] "+ collision.gameObject.name + " - Tank : " +tank +" - ID : "+ ((tank)? tank.ID:-1) + " - Lanceur :" + handler );
        if (tank)
        {
            if (handler == 0) return;
            if (handler == tank.ID) return;
            tank.dealDamage(damage, handler);
        }     
        Destroy(gameObject);
    }
}
