using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class InvisibleItem : NetworkBehaviour
{
    public float time = 5.0f;

    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Tank>())
        {
            other.GetComponent<Tank>().invisibility = time;
            Destroy(gameObject);
        }
    }
}
