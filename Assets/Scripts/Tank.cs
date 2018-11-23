using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour {
    
	public void forward()
    {
       transform.position += Vector3.forward;
    }

    public void backward()
    {
        transform.position += -1 * transform.forward;
    }

    public void left()
    {
        transform.position += -1 * transform.right;
    }

   public  void right()
    {
        transform.position += transform.right;
    }
}
