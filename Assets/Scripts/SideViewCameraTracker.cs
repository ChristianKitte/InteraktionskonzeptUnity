using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideViewCameraTracker : MonoBehaviour
{
    [SerializeField] private Camera Camera;

    private void Awake()
    {
        Camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (InteractionManager.Instance.SelectedObject != null)
        {
            Transform selectedTransform = InteractionManager.Instance.SelectedObject.transform;

            float Abstand = 10.0f;

            Camera.transform.position = new Vector3(
                selectedTransform.position.x + Vector3.right.x * Abstand,
                selectedTransform.position.y,
                selectedTransform.position.z);

            Camera.transform.LookAt(selectedTransform.position);
            Camera.enabled = true;
        }
        else
        {
            Camera.enabled = false;
        }
    }
}