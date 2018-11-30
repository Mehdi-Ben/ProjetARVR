using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public Vector3 vect;
    public float speed;

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
        if (collision.gameObject.GetComponent<Tank>())
        {
            Destroy(collision.gameObject);
        }
        Destroy(gameObject);
    }
}
