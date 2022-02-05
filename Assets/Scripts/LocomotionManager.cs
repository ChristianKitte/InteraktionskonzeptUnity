using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocomotionManager : MonoBehaviour
{
    private GameObject _selectedObject = null;
    private GameObject _activeObject = null;

    private bool _objectAvailable = false;
    private bool _triggerActive = false;
    private bool _performGrip = false;

    public GameObject ActiveObject
    {
        get => _activeObject;
        set => _activeObject = value;
    }

    public bool TriggerIsActive
    {
        get => _triggerActive;
        set => _triggerActive = value;
    }

    public void performSelection()
    {
        if (ActiveObject != null && TriggerIsActive)
        {
            _selectedObject = _activeObject;
        }
        else if (!TriggerIsActive)
        {
            _selectedObject = null;
        }
    }

    public void WriteDebugMessage()
    {
        if (_activeObject != null)
        {
            Debug.Log("Active object is: " + _activeObject.name);
        }
        else
        {
            Debug.Log("No active object");
        }

        if (_selectedObject != null)
        {
            Debug.Log("Selected object is: " + _selectedObject.name);
        }
        else
        {
            Debug.Log("No selecetd object");
        }

        Debug.Log("Trigger is: " + _triggerActive.ToString());
    }
}