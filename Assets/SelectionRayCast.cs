using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionRayCast : MonoBehaviour
{
    public Transform startRay;
    private GameObject selectedObject;
    private Color gameObjectOriginColor;

    // Update is called once per frame
    void Update()
    {
        if (selectedObject != null)
        {
            selectedObject.GetComponent<Renderer>().material.color = gameObjectOriginColor;
        }
    }

    private void LateUpdate()
    {

        
         // gibt True zurück, sofern eine Kollision stattgefunden hat
         // hit enthält in diesem Fall ein RaycastHit Object
         if (Physics.Raycast(
             startRay.transform.position,
             startRay.transform.forward,
             out var hit,
             100f
         ))
         {
             Debug.Log(hit.collider.name);
             selectedObject = hit.collider.gameObject;
             gameObjectOriginColor = hit.transform.gameObject.GetComponent<Renderer>().material.color;
 
             hit.transform.gameObject.GetComponent<Renderer>().material.color = Color.yellow;
         }
         else
         {
             Debug.Log("Kein Kollision");
 
             selectedObject = null;
         }       
    }
}