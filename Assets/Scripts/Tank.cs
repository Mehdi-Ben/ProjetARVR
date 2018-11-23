using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Tank : NetworkBehaviour {
    static public Dictionary<string, int> input = new Dictionary<string, int>();
    public float speed;

    
    private void Update()
    {
        /*  if (forwardTouch.activate) forward();
          if (backwardTouch.activate) backward();
          if (leftTouch.activate) left();
          if (rightTouch.activate) right();*/
        if (!Tank.input.ContainsKey("AxisVertical"))
        {
            Tank.input.Add("AxisVertical", 0);
            Tank.input.Add("AxisHorizontal", 0);
        }
        if (isLocalPlayer)
        {
            transform.position += (Vector3.forward * input["AxisVertical"] + Vector3.right * input["AxisHorizontal"]).normalized * Time.deltaTime * speed;
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
