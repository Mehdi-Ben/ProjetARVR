using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameManager : NetworkBehaviour
{
    public static GameManager instance;
    [SyncVar] public int numberPlayer;

    public void Start()
    {
        if (instance)
        {
            instance = this;
        }
    }
}
