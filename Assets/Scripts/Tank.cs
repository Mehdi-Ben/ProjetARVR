using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Tank : NetworkBehaviour
{

    public Color[] colors = { Color.blue, Color.red, Color.green, Color.yellow, Color.cyan, Color.black, Color.magenta, Color.grey};
    public int ID;
    public GameObject bullet;
    private Rigidbody m_Rigidbody;
    public float speed = 6.0f;
    public float turnspeed = 6.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;

    public GameObject trackerPrefab;
    public float distanceTracker;
    public Transform targetTracker;
    public GameObject tracker;

    private Vector3 moveDirection = Vector3.zero;
    public bool debug;

    private void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        ID = (int)netId.Value;
        GetComponent<MeshRenderer>().material.color = colors[ID% colors.Length];
        tracker = Instantiate(trackerPrefab);
        targetTracker = transform;
        //transform.parent = GameObject.Find("ImageTarget").transform;
    }

    private void Update()
    {
        if (transform.position.y < -2f)
        {
            NetworkStartPosition[] spawnPoints = FindObjectsOfType<NetworkStartPosition>();
            transform.position = spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position;
            transform.rotation = Quaternion.identity;
            m_Rigidbody.velocity = Vector3.zero;
        }
        if (isLocalPlayer || debug)
        {
            
                // We are grounded, so recalculate
                // move direction directly from axes

                moveDirection = new Vector3(Joystick.direction.x, 0.0f, Joystick.direction.z);
            //moveDirection = transform.TransformDirection(moveDirection);
            if (moveDirection.magnitude < 0.02f) return;
                moveDirection = moveDirection * speed;

                if (Input.GetButton("Jump"))
                {
                    moveDirection.y = jumpSpeed;
                }

            // Apply gravity
            //moveDirection.y = moveDirection.y - (gravity * Time.deltaTime);

            // Move the controller

            //transform.LookAt(transform.position + moveDirection);
            moveDirection = (Camera.main.transform.forward * moveDirection.z + Camera.main.transform.right * moveDirection.x);

            Vector3 vect = m_Rigidbody.position + moveDirection;
            
            m_Rigidbody.MovePosition(vect);

            float d = 0;
            d += Vector3.Dot(transform.right, moveDirection.normalized);
            if (Vector3.Dot(transform.forward, moveDirection.normalized) < 0)
            {
                d += Mathf.Sign(d) * 1;
            }
            

            print(d);
            transform.Rotate(transform.up, turnspeed * d);

            //transform.LookAt(transform.forward);
            Debug.DrawRay(transform.position, moveDirection.normalized * 5, Color.blue);
            Debug.DrawRay(transform.position, transform.forward * 5, Color.red);
            Debug.DrawRay(transform.position, (transform.forward * moveDirection.z + transform.right * moveDirection.x).normalized * 5, Color.green);

            tracker.transform.position = (new Vector3(targetTracker.position.x, 0, targetTracker.position.z)).normalized * distanceTracker;
            tracker.transform.LookAt(Vector3.zero);

        }

    }
    

}
