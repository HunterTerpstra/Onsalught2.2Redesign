using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PopupPanel : MonoBehaviour
{
    GameObject origin;

    public void Setup(GameObject source_object, Vector3 offset, int[] elements)
    {
        Debug.LogFormat("Popup panel created @ {0}", offset);
        // set panel size based on contained elements

        transform.position = source_object.transform.position + offset;
    }

    void Update()
    {
        
    }
}
