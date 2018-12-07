using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class AttackUp : NetworkBehaviour
{
    public float time = 15.0f;

    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Tank>())
        {
            other.GetComponent<Tank>().attackUp = time;
            Destroy(gameObject);
        }
    }
}
