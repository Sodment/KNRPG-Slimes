using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FastActiveButton : MonoBehaviour
{
    public UnityEvent PressedEvent;
    private void OnMouseDown()
    {
        Debug.Log("Pressed");
        PressedEvent.Invoke();
    }
}
