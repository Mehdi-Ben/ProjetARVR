using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DisconnectButton : NetworkBehaviour {

    public void Disconnect()
    {
        NetworkManager netManager = NetworkManager.singleton;
        if (isServer)
        {
            netManager.StopHost();
        }
        else
        {
            NetworkClient.ShutdownAll();
            netManager.StopClient();
        }
    }
}
