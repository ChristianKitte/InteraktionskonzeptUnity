using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.XR.Interaction.Toolkit;

/// <summary>
/// Erzeugt einen RayCast auf Basis der Parameter und hebt mögliche Selektionen
/// durch Setzen von Emission hervor. Hierfür muss eine Color für Emission gesetzt
/// werden. Die default Color ist Weiß. Mögliche Objekte verfügen über das angegebnen
/// Tag und einen Renderer. 
/// </summary>
public class SelectionRayCast : MonoBehaviour
{
    [SerializeField] private Transform startRay;
    [SerializeField] private float rayLength = 100f;
    [SerializeField] private string selectionTag;
    [SerializeField] private LocomotionManager locomotionManager;

    private GameObject _selectedObject;
    private Renderer _currentRenderer;

    private LocomotionManager _locomotionManager;

    private void Awake()
    {
        _locomotionManager = locomotionManager.GetComponent<LocomotionManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_locomotionManager.TriggerIsActive && _selectedObject != null && _currentRenderer != null)
        {
            _selectedObject.GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
        }
    }

    // LastUpdate is called once per frame if Behaviour is enabled
    private void LateUpdate()
    {
        // Physics.Raycast gibt True zurück, sofern eine Kollision stattgefunden hat.
        // Die Out Variable hit enthält in diesem Fall ein RaycastHit Object.
        if (_locomotionManager.TriggerIsActive && Physics.Raycast(
            startRay.transform.position,
            startRay.transform.forward,
            out var hit,
            rayLength
        ))
        {
            _selectedObject = hit.transform.gameObject;
            if (_selectedObject != null)
            {
                _currentRenderer = _selectedObject.GetComponent<Renderer>();

                if (_currentRenderer != null && _selectedObject.CompareTag(selectionTag))
                {
                    // Debug.Log("Select :" + hit.collider.name);
                    _selectedObject.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
                    _locomotionManager.ActiveObject = _selectedObject;

                    Debug.Log("Objekt gefunden...");
                }
            }
        }
        else
        {
            // Debug.Log("No collision or no trigger");
            _selectedObject = null;
        }

        _locomotionManager.WriteDebugMessage();
    }
}