using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.XR.Interaction.Toolkit;

/// <summary>
/// In Projekt Settings vor Default Ausführung gesetzt.
/// </summary>
public class DefaultInputActionsController : MonoBehaviour
{
    private XRIDefaultInputActions _controls;
    private XRRayInteractor _rayInteractor;

    [SerializeField] GameObject leftController;

    [SerializeField] GameObject rightController;

    private void OnEnable()
    {
        _controls.XRILeftHand.Enable();
    }

    private void OnDisable()
    {
        _controls.XRILeftHand.Disable();
    }

    private void Awake()
    {
        _rayInteractor = leftController.GetComponent<XRRayInteractor>();
        _controls = new XRIDefaultInputActions();

        _controls.XRILeftHand.Select.started += ctx => selectionStarted();
    }

    private void Update()
    {
        _rayInteractor.enabled = _controls.XRILeftHand.Activate.IsPressed();
        InteractionManager.Instance.TriggerIsActive = _controls.XRILeftHand.Activate.IsPressed();
        InteractionManager.Instance.GripIsActive = _controls.XRILeftHand.Select.IsPressed();
    }

    private void selectionStarted()
    {
        InteractionManager.Instance.StartSelection = true;
    }
}