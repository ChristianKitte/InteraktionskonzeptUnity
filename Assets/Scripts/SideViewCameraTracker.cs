using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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

            var position = selectedTransform.position;
            Camera.transform.position = new Vector3(
                position.x + Vector3.right.x * Abstand,
                position.y,
                position.z);

            Camera.transform.LookAt(position);
            Camera.transform.Rotate(0, 0, 180);

            Camera.enabled = true;
        }
        else
        {
            Camera.enabled = false;
        }
    }
}