using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ItemManager : NetworkBehaviour
{
    public RatioItem[] items;
    public float chanceItem;
    public float timeItem;
    public float t;
    public Vector2 minPos;
    public Vector2 maxPos;
    public float sum;
	// Use this for initialization
	void Start ()
    {
        t = timeItem;
        sum = 0;
        foreach (RatioItem i in items)
        {
            sum += i.ratio;
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        t -= Time.deltaTime;
        if (t <= 0)
        {
            t = timeItem;
            if (Random.Range(0f, 1f) < chanceItem)
            {
                CreateItem();
            }
        }
	}

    public void CreateItem()
    {
        float r = Random.Range(0f, sum);
        foreach (RatioItem i in items)
        {
            r -= i.ratio;
            if (r <= 0)
            {
                GameObject item = Instantiate(i.item, new Vector3(Random.Range(minPos.x, maxPos.x), 10, Random.Range(minPos.y, maxPos.y)), Quaternion.identity);
                NetworkServer.Spawn(item);
                Destroy(item, 25);
                return;
            }
        }
    }
}

[System.Serializable]
public struct RatioItem
{
    public GameObject item;
    [Range(0f,1f)]
    public float ratio;
}
