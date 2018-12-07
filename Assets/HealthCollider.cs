using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class HealthCollider : NetworkBehaviour
{
    public float value;

    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Tank>())
        {
            other.GetComponent<Tank>().PV = Mathf.Min(100, other.GetComponent<Tank>().PV + value);
            Destroy(gameObject);
        }
    }
}
