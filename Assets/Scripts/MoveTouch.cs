using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveTouch : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string input;
    public int value;


    private void OnPointerEnter()
    {
        
        Tank.input[input] = value;
    }
  

    public void OnPointerEnter(PointerEventData eventData)
    {
        Tank.input[input] = value;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Tank.input[input] = 0;
    }
}
