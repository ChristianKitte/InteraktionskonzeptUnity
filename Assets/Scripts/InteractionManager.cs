using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    private GameObject _selectedObject = null;
    private GameObject _activeObject = null;
    private bool _triggerActive = false;

    public GameObject SelectedObject
    {
        get => _selectedObject;
        set => _selectedObject = value;
    }

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

    private void LateUpdate()
    {
        if (_activeObject != null)
        {
            if (_selectedObject == null && TriggerIsActive)
            {
                _selectedObject = _activeObject;
                _selectedObject.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");

                Debug.Log("Selected Obj is null");
                Debug.Log("1");
            }
            else if (_selectedObject != null && !_selectedObject.Equals(_activeObject) && TriggerIsActive)
            {
                _selectedObject.GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
                _selectedObject = _activeObject;
                _selectedObject.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");

                Debug.Log(_selectedObject.name);
                Debug.Log("2");
            }
            else if (SelectedObject != null && _selectedObject.Equals(_activeObject) && TriggerIsActive)
            {
                Debug.Log(_selectedObject.name);

                _selectedObject.GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
                _selectedObject = null;

                Debug.Log("3");
            }
        }
    }

    public void performSelection()
    {/*
        if (_activeObject != null)
        {
            if (_selectedObject == null && TriggerIsActive)
            {
                _selectedObject = _activeObject;
                _selectedObject.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");

                Debug.Log("Selected Obj is null");
                Debug.Log("1");
            }
            else if (_selectedObject != null && !_selectedObject.Equals(_activeObject) && TriggerIsActive)
            {
                _selectedObject.GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
                _selectedObject = _activeObject;
                _selectedObject.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");

                Debug.Log(_selectedObject.name);
                Debug.Log("2");
            }
            else if (SelectedObject != null && _selectedObject.Equals(_activeObject) && TriggerIsActive)
            {
                Debug.Log(_selectedObject.name);

                _selectedObject.GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
                _selectedObject = null;

                Debug.Log("3");
            }
        }*/
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