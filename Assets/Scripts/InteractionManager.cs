using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    private GameObject _selectedObject = null;
    private GameObject _currentObject = null;

    private bool _leftLeftTriggerIsActive = false;
    private bool _leftGripIsActive = false;
    private bool _rightGripIsActive = false;
    private bool _startSelection = false;

    public static InteractionManager Instance { get; private set; }

    public GameObject SelectedObject
    {
        get => _selectedObject;
        set => _selectedObject = value;
    }

    public GameObject CurrentObject
    {
        get => _currentObject;
        set => _currentObject = value;
    }

    public bool LeftTriggerIsActive
    {
        get => _leftLeftTriggerIsActive;
        set => _leftLeftTriggerIsActive = value;
    }

    public bool LeftGripIsActive
    {
        get => _leftGripIsActive;
        set => _leftGripIsActive = value;
    }

    public bool RightGripIsActive
    {
        get => _rightGripIsActive;
        set => _rightGripIsActive = value;
    }

    public bool StartSelection
    {
        get => _startSelection;
        set => _startSelection = value;
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }

        //DontDestroyOnLoad(InteractionManager.Instance); // es gibt nur eine Szene...
        Instance = this;
    }

    private void LateUpdate()
    {
        if (_startSelection && _leftLeftTriggerIsActive)
        {
            if (_currentObject == null) // fertig
            {
                _startSelection = false;
            }
            else if (_currentObject != null)
            {
                if (_selectedObject == null) // selektieren
                {
                    _selectedObject = _currentObject;
                    _selectedObject.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
                    _selectedObject.GetComponent<Rigidbody>().isKinematic = true;

                    _startSelection = false;
                    _currentObject = null;
                }
                else if (_selectedObject.Equals(_currentObject)) // deselektieren
                {
                    _selectedObject.GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
                    _selectedObject.GetComponent<Rigidbody>().isKinematic = false;
                    _selectedObject = null;

                    _startSelection = false;
                    _currentObject = null;
                }
                else if (!_selectedObject.Equals(_currentObject)) // deselektieren und selektieren
                {
                    _selectedObject.GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
                    _selectedObject.GetComponent<Rigidbody>().isKinematic = false;
                    _selectedObject = null;

                    _selectedObject = _currentObject;
                    _selectedObject.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
                    _selectedObject.GetComponent<Rigidbody>().isKinematic = true;

                    _startSelection = false;
                    _currentObject = null;
                }
            }
        }
    }

    public void MoveSelectedObject(Vector3 move3DVector)
    {
    }

    public void WriteDebugMessage()
    {
        if (_currentObject != null)
        {
            Debug.Log("Active object is: " + _currentObject.name);
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

        Debug.Log("Trigger is: " + _leftLeftTriggerIsActive.ToString());
        Debug.Log("Trigger is: " + _startSelection.ToString());
    }
}