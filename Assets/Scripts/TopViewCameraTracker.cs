using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopViewCameraTracker : MonoBehaviour
{
    [SerializeField] private Camera Camera;
    [SerializeField] private float abstandY = 10.0f;
    [SerializeField] private Light topLight;

    private void Awake()
    {
        Camera = GetComponent<Camera>();
    }

    private void Update()
    {
        if (InteractionManager.Instance.SelectedObject != null)
        {
            Transform selectedTransform = InteractionManager.Instance.SelectedObject.transform;

            var position = selectedTransform.position;
            Camera.transform.position = new Vector3(
                position.x,
                position.y + Vector3.up.y * abstandY,
                position.z);

            Camera.transform.LookAt(position);
            Camera.transform.Rotate(0, 180, 0, Space.World);

            topLight.transform.position = Camera.transform.position;

            Camera.enabled = true;
            topLight.enabled = true;
        }
        else
        {
            Camera.enabled = false;
            topLight.enabled = false;
        }
    }
}