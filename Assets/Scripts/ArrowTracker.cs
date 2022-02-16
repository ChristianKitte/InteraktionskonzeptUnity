using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTracker : MonoBehaviour
{
    [SerializeField] private GameObject forwardArrowObject;
    [SerializeField] private GameObject rightArrowObject;

    void Update()
    {
        Vector3 arrowPosition = forwardArrowObject.transform.position;

        Vector3 lookForwardVector = new Vector3(arrowPosition.x + 0.1f, arrowPosition.y, arrowPosition.z);
        forwardArrowObject.transform.LookAt(lookForwardVector);
        forwardArrowObject.transform.Rotate(0, 180, 90, Space.World);

        //rightArrowObject.transform.Rotate(90, 180, 0, Space.World);
    }
}