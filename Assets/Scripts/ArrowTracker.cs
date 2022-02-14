using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTracker : MonoBehaviour
{
    [SerializeField] private GameObject arrowObject;

    void Update()
    {
        Vector3 arrowPosition = arrowObject.transform.position;
        Vector3 lookAtVector = new Vector3(arrowPosition.x + 0.1f, arrowPosition.y, arrowPosition.z);

        arrowObject.transform.LookAt(lookAtVector);
        arrowObject.transform.Rotate(0, 180, 90, Space.World);
    }
}