using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Tank : NetworkBehaviour
{


    public static Color[] colors = { new Color(0.5f,0.5f,1f), new Color(1f, 0.5f, 0.5f) ,
                                    new Color(0.5f, 1f, 0.5f) , new Color(1f, 1f, 0.5f),
                                    new Color(0.5f,1f,1f), new Color(1f, 0.5f, 0f) ,
                                    new Color(0.3f, 0.4f, 0.5f) , new Color(0.7f, 0.7f, 0.7f)};
    public Color[] colorsTest = { new Color(0.5f,0.5f,1f), new Color(1f, 0.5f, 0.5f) ,
                                    new Color(0.5f, 1f, 0.5f) , new Color(1f, 1f, 0.5f),
                                    new Color(0.5f,1f,1f), new Color(1f, 0.5f, 0f) ,
                                    new Color(0.3f, 0.4f, 0.5f) , new Color(0.7f, 0.7f, 0.7f)};
    public MeshRenderer[] meshes;
    public int ID;
    public GameObject bulletPrefab;
    private Rigidbody m_Rigidbody;
        
    public Transform spawnBullet;
    public GameObject trackerPrefab;
    public GameObject shadowPrefab;
    public GameObject shadow;
    public float distanceTracker;
    public GameObject tracker;
    private Vector3 moveDirection = Vector3.zero;
    public bool debug;

    [Header("Stats")]
    public float speed = 6.0f;
    public float turnspeed = 6.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public float reloadTime = 0.1f;



    [Header("Sync")]
    [SyncVar()] public float PV = 100;
    [SyncVar()] public int score = 0;
    [SyncVar()] public Vector3 targetTracker;

    [Header("HUD")]
    public Text textScore;
    public Text textPV;
    public Image gaugeImage;
    public GameObject arrowTracker;
    


    public static bool buttonFire;
    private float t;

    private void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        ID = IDPlayer()+1;
        tracker = Instantiate(trackerPrefab);
        tracker.GetComponent<TagSprite>().playerColor = colors[(ID - 1)];
        tracker.GetComponent<TagSprite>().playerId = ID;
        shadow = Instantiate(shadowPrefab);

        if (isLocalPlayer)
        {
            arrowTracker.GetComponent<MeshRenderer>().material.color = colors[(ID - 1)];
            textScore = GameObject.Find("TextScore").GetComponent<Text>();
            textPV = GameObject.Find("PVDisplay").GetComponent<Text>();
            gaugeImage = GameObject.Find("PVGauge").GetComponent<Image>();
            textScore.color = colors[(ID - 1)];
            gaugeImage.color = colors[(ID - 1)];
        }
        else
        {
            Destroy(arrowTracker);
        }

       
        foreach (MeshRenderer m in meshes)
        {
            m.material.color = colors[(ID - 1)];
        }
        //transform.parent = GameObject.Find("ImageTarget").transform;
    }

    private int IDPlayer()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0 ; i < players.Length; i++)
        {
            if (players[i] == gameObject) return i;
        }
        return -1;
    }



    private void Update()
    {
        CmdUpdateCamera();
        ShadowBlob();
        t -= Time.deltaTime;
        if (transform.position.y < -2f)
        {
            score--;
            PV = 100;
            RpcRespawn();
        }
        tracker.transform.position = (new Vector3(targetTracker.x, 0, targetTracker.z)).normalized * distanceTracker;
        tracker.transform.LookAt(Vector3.zero);
        Vector3 TmoveDirection = (Camera.main.transform.forward+ Camera.main.transform.right);
        TmoveDirection = (new Vector3(TmoveDirection.x, 0, TmoveDirection.z)).normalized;
        Debug.DrawRay(transform.position, TmoveDirection * 5, Color.blue);
        Debug.DrawRay(transform.position, transform.forward * 5, Color.red);
        Debug.DrawRay(transform.position, (transform.forward * moveDirection.z + transform.right * moveDirection.x).normalized * 5, Color.green);

        if (isLocalPlayer || debug)
        {
            textPV.text = PV.ToString("00");
            gaugeImage.fillAmount -= (gaugeImage.fillAmount - ((1.0f * PV) / 100.0f)) * 0.1f;
            textScore.text = score.ToString("00");
            if (buttonFire &&  t < 0)
            {
                t = reloadTime;
                print("A");
                CmdFire();
                buttonFire = false;
            }
            moveDirection = new Vector3(Joystick.direction.x, 0.0f, Joystick.direction.z);
            //moveDirection = transform.TransformDirection(moveDirection);
            //targetTracker = Camera.main.transform.position;
            if (moveDirection.magnitude < 0.02f) return;
            moveDirection = moveDirection * speed;
            moveDirection = (Camera.main.transform.forward * moveDirection.z + Camera.main.transform.right * moveDirection.x);
            moveDirection = (new Vector3(moveDirection.x, 0, moveDirection.z)).normalized;
            Vector3 vect = m_Rigidbody.position + moveDirection*Time.deltaTime*speed;
            
            m_Rigidbody.MovePosition(vect);

            float d = 0;
            d += Vector3.Dot(transform.right, moveDirection.normalized);
            if (Vector3.Dot(transform.forward, moveDirection.normalized) < 0)
            {
                d += Mathf.Sign(d) * 1;
            }
            

            print(d);
            transform.Rotate(transform.up, turnspeed * d);

        }
        
    }
    

    public void ShadowBlob()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, Vector3.down);
        ;
        if (Physics.Raycast(ray, out hit))
        {
            Debug.DrawRay(hit.point, ray.direction* hit.distance, Color.green);
            shadow.transform.up = hit.normal;
            shadow.transform.position = hit.point+ new Vector3(0, 0.01f, 0);
        }
    }
    public void dealDamage(float damage, int player)
    {
        PV -= damage;
        if (PV <= 0)
        {
            score--;
            PV = 100;
            if (player != -1)
            {
                GameObject.FindGameObjectsWithTag("Player")[player-1].GetComponent<Tank>().score++;
            }
            RpcRespawn();

        }
    }

    [ClientRpc]
    void RpcRespawn()
    {
        if (isLocalPlayer)
        {
            NetworkStartPosition[] spawnPoints = FindObjectsOfType<NetworkStartPosition>();
            transform.position = spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position;
            transform.rotation = Quaternion.identity;
            m_Rigidbody.velocity = Vector3.zero;
        }
    }

    [Command]
    public void CmdFire()
    {
        
        GameObject bullet = Instantiate(bulletPrefab);
        bullet.transform.position = spawnBullet.position;

        bullet.GetComponent<Bullet>().handler = ID;
        bullet.GetComponent<Bullet>().speed = 20;
        bullet.GetComponent<Bullet>().direction = transform.forward;
        NetworkServer.Spawn(bullet);
    }

    [Command]
    public void CmdUpdateCamera()
    {
       if (isLocalPlayer) targetTracker = Camera.main.transform.position;
    }

    public void FireButton()
    {
        print("Ok");
        buttonFire = true;
    }
}
