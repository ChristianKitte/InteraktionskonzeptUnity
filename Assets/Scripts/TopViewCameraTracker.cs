using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopViewCameraTracker : MonoBehaviour
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
                position.x,
                position.y + Vector3.up.y * Abstand,
                position.z);

            Camera.transform.LookAt(position);

            Camera.enabled = true;
        }
        else
        {
            Camera.enabled = false;
        }
    }
}