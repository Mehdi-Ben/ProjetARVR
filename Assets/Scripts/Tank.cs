using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Tank : NetworkBehaviour {
    static public Dictionary<string, float> input = new Dictionary<string, float>();
    public Color[] colors = { Color.blue, Color.red, Color.green, Color.yellow };
    public float speed;
    public int ID;

    private void Start()
    {
        
        ID = (int)netId.Value;
        GetComponent<MeshRenderer>().material.color = colors[ID% colors.Length];
        transform.parent = GameObject.Find("ImageTarget").transform;
    }

    private void Update()
    {
        if (transform.position.y < 0)
        {
            transform.position = new Vector3(0, 10, 0);
            transform.rotation = Quaternion.identity;
        }
        if (isLocalPlayer)
        {
            Vector3 vect = new Vector3(-Joystick.direction.z,0, Joystick.direction.x).normalized ;

            Vector3 forward = Camera.main.transform.forward;
            Vector3 right = Camera.main.transform.right;

            forward = (new Vector3(-forward.x, 0, -forward.z)).normalized;
            right = (new Vector3(right.x, 0, right.z)).normalized;


            transform.position += (forward * vect.x + right * vect.z).normalized * Time.deltaTime * speed;
            transform.LookAt(transform.position + (forward * vect.x + right * vect.z));
        }

    }

    public void forward()
    {
       transform.position += Vector3.forward * Time.deltaTime;
    }

    public void backward()
    {
        transform.position += -1 * transform.forward * Time.deltaTime;
    }

    public void left()
    {
        transform.position += -1 * transform.right * Time.deltaTime;
    }

   public  void right()
    {
        transform.position += transform.right * Time.deltaTime;
    }
}
