using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SideViewCameraTrackerZ : MonoBehaviour
{
    [SerializeField] private Camera Camera;
    [SerializeField] private float abstandZ = 10.0f;
    [SerializeField] private float offset = 1.0f;
    [SerializeField] private float rotation = 3.0f;

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

            var position = selectedTransform.position;
            Camera.transform.position = new Vector3(
                position.x,
                position.y + offset,
                position.z + Vector3.back.z * abstandZ);

            Camera.transform.LookAt(position);
            Camera.transform.Rotate(rotation, 0, 180);

            Camera.enabled = true;
        }
        else
        {
            Camera.enabled = false;
        }
    }
}