using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.Networking.Match;

public class NetworkHUD : NetworkManager
{
    void Start()
    {
        NetworkManager.singleton.StartMatchMaker();
    }

    //call this method to request a match to be created on the server
    public void CreateInternetMatch(string matchName)
    {
        NetworkManager.singleton.matchMaker.CreateMatch(matchName, 4, true, "", "", "", 0, 0, OnInternetMatchCreate);
    }

    //this method is called when your request for creating a match is returned
    private void OnInternetMatchCreate(bool success, string extendedInfo, MatchInfo matchInfo)
    {
        if (success)
        {
            //Debug.Log("Create match succeeded");

            MatchInfo hostInfo = matchInfo;
            NetworkServer.Listen(hostInfo, 9000);

            NetworkManager.singleton.StartHost(hostInfo);
        }
        else
        {
            Debug.LogError("Create match failed");
        }
    }

    private void OnInternetServerMatchCreate(bool success, string extendedInfo, MatchInfo matchInfo)
    {
        if (success)
        {
            //Debug.Log("Create match succeeded");

            MatchInfo hostInfo = matchInfo;
            NetworkServer.Listen(hostInfo, 9000);

            NetworkManager.singleton.StartServer(hostInfo);
        }
        else
        {
            Debug.LogError("Create match failed");
        }
    }

    //call this method to find a match through the matchmaker
    public void FindInternetMatch(string matchName)
    {
        NetworkManager.singleton.matchMaker.ListMatches(0, 10, matchName, true, 0, 0, OnInternetMatchList);
    }

    //this method is called when a list of matches is returned
    private void OnInternetMatchList(bool success, string extendedInfo, List<MatchInfoSnapshot> matches)
    {
        if (success)
        {
            if (matches.Count != 0)
            {
                //Debug.Log("A list of matches was returned");

                //join the last server (just in case there are two...)
                NetworkManager.singleton.matchMaker.JoinMatch(matches[matches.Count - 1].networkId, "", "", "", 0, 0, OnJoinInternetMatch);
            }
            else
            {
                Debug.Log("No matches in requested room!");
            }
        }
        else
        {
            Debug.LogError("Couldn't connect to match maker");
        }
    }

    //this method is called when your request to join a match is returned
    private void OnJoinInternetMatch(bool success, string extendedInfo, MatchInfo matchInfo)
    {
        if (success)
        {
            //Debug.Log("Able to join a match");

            MatchInfo hostInfo = matchInfo;
            NetworkManager.singleton.StartClient(hostInfo);
        }
        else
        {
            Debug.LogError("Join match failed");
        }
    }




    //call this method to find a match through the matchmaker
    public void FindInternetMatch()
    {
        NetworkManager.singleton.matchMaker.ListMatches(0, 10, "Test", true, 0, 0, OnInternetMatchList);
    }

    //call this method to request a match to be created on the server
    public void CreateInternetMatch()
    {
        NetworkManager.singleton.matchMaker.CreateMatch("Test", 4, true, "", "", "", 0, 0, OnInternetMatchCreate);
    }

    public void CreateServerMatch()
    {
        NetworkManager.singleton.matchMaker.CreateMatch("Test", 4, true, "", "", "", 0, 0, OnInternetServerMatchCreate);
    }
}