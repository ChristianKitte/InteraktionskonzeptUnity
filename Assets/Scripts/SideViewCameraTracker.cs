using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideViewCameraTracker : MonoBehaviour
{
    private Camera _camera;

    // Start is called before the first frame update
    private void Awake()
    {
        _camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (InteractionManager.Instance.SelectedObject != null)
        {
            Transform selectedTransform = InteractionManager.Instance.SelectedObject.transform;

            _camera.transform.SetParent(selectedTransform);

            float Abstand = 100.0f;

            Vector3 relativePosition = new Vector3(_camera.transform.position.x + Abstand, _camera.transform.position.y,
                _camera.transform.position.z);

            _camera.transform.position = relativePosition;

            transform.LookAt(selectedTransform.position, Vector3.up);
        }

        _camera.enabled = true;
    }
}