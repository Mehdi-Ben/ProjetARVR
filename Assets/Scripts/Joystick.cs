using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public Image imageJoystick;
    public Vector2 vect;
    public Vector2 pos;

    public float deltaLength = 100f;
    static public Vector3 direction;
    

    public void OnDrag(PointerEventData eventData)
    {
        print("OK");
        vect = eventData.position;
        pos = transform.position;
        Vector2 currentVect = (eventData.position - pos);
        Vector2 direc = currentVect / deltaLength;

        if (direc.magnitude > 1f) direc.Normalize();

        imageJoystick.transform.position = direc * deltaLength + pos;
        direction = new Vector3(direc.x, 0, direc.y);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        direction = Vector3.zero;
        imageJoystick.transform.position = transform.position;
    }
    
}
