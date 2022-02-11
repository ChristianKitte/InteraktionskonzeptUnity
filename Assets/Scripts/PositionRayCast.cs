using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PositionRayCast : MonoBehaviour
{
    [SerializeField] private LineRenderer PositionLineRenderer;
    [SerializeField] private float ActivateDistance = 2.5f;

    private void Start()
    {
        PositionLineRenderer = GetComponent<LineRenderer>();
    }

    void Update()
    {
        PositionLineRenderer.enabled = false;
        InteractionManager.Instance.GroundDistanceString = "no ground";

        if (InteractionManager.Instance.SelectedObject != null)
        {
            Transform selectedObjectTransform = InteractionManager.Instance.SelectedObject.transform;

            RaycastHit hitInfo;
            if (Physics.Raycast(selectedObjectTransform.position, Vector3.down, out hitInfo))
            {
                if (hitInfo.distance <= ActivateDistance)
                {
                    Vector3 _startRayVector3D = selectedObjectTransform.position;
                    Vector3 _endRayVector3D = new Vector3(_startRayVector3D.x, hitInfo.point.y,
                        _startRayVector3D.z);

                    PositionLineRenderer.SetPositions(new[] { _startRayVector3D, _endRayVector3D });
                    PositionLineRenderer.enabled = true;

                    InteractionManager.Instance.GroundDistanceString = hitInfo.distance.ToString();
                }
            }
        }
    }
}