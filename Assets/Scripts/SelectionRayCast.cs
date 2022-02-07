using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.XR.Interaction.Toolkit;

public class SelectionRayCast : MonoBehaviour
{
    [SerializeField] private Transform startRay;
    [SerializeField] private float rayLength = 100f;
    [SerializeField] private string selectionTag;
    [SerializeField] private InteractionManager interactionManager;

    private GameObject _currentObject;
    private Renderer _currentRenderer;

    // Update is called once per frame
    void Update()
    {
        if (InteractionManager.Instance.TriggerIsActive && _currentObject != null && _currentRenderer != null)
        {
            _currentObject.GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
        }
    }

    private void LateUpdate()
    {
        // Physics.Raycast gibt True zurück, sofern eine Kollision stattgefunden hat.
        // Die Out Variable hit enthält in diesem Fall ein RaycastHit Object.
        if (InteractionManager.Instance.TriggerIsActive && Physics.Raycast(
            startRay.transform.position,
            startRay.transform.forward,
            out var hit,
            rayLength
        ))
        {
            _currentObject = hit.transform.gameObject;
            //Debug.Log("_currentObject1: " + _currentObject);

            if (_currentObject != null)
            {
                _currentRenderer = _currentObject.GetComponent<Renderer>();

                if (_currentRenderer != null && _currentObject.CompareTag(selectionTag))
                {
                    _currentObject.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
                    InteractionManager.Instance.CurrentObject = _currentObject;
                }
            }
        }
        else
        {
            _currentObject = null;
        }
    }
}