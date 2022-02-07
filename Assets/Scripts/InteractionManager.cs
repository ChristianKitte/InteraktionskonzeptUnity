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

    private bool _triggerIsActive = false;
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

    public bool TriggerIsActive
    {
        get => _triggerIsActive;
        set => _triggerIsActive = value;
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
        if (_startSelection && _triggerIsActive)
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

                    _startSelection = false;
                }
                else if (_selectedObject.Equals(_currentObject)) // deselektieren
                {
                    _selectedObject.GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
                    _selectedObject = null;

                    _startSelection = false;
                }
                else if (!_selectedObject.Equals(_currentObject)) // deselektieren und selektieren
                {
                    _selectedObject.GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
                    _selectedObject = null;

                    _selectedObject = _currentObject;
                    _selectedObject.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");

                    _startSelection = false;
                }

                var text = GameObject.Find("SelectionInfo").GetComponent<TextMeshPro>();
                if (text != null)
                {
                    if (_selectedObject != null)
                    {
                        text.text = _selectedObject.name;
                    }
                    else
                    {
                        text.text = "kein Objekt";
                    }
                }
            }
        }
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

        Debug.Log("Trigger is: " + _triggerIsActive.ToString());
        Debug.Log("Trigger is: " + _startSelection.ToString());
    }
}