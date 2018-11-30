using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public GameObject handler;
    public Vector3 vect;
    public float speed;
    public float damage;

    private void Start()
    {
        Destroy(gameObject, 5f);
    }
    // Update is called once per frame
    void Update ()
    {
        transform.position += vect * Time.deltaTime * speed;
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.Equals(handler)) return;
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
