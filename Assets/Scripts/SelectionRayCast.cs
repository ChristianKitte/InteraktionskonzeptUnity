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

    void Update()
    {
        if (InteractionManager.Instance.LeftTriggerIsActive && _currentObject != null && _currentRenderer != null)
        {
            _currentObject.GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
        }
    }

    private void LateUpdate()
    {
        if (InteractionManager.Instance.LeftTriggerIsActive && Physics.Raycast(
            startRay.transform.position,
            startRay.transform.forward,
            out var hit,
            rayLength
        ))
        {
            _currentObject = hit.transform.gameObject;

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