using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.XR.Interaction.Toolkit;

public class DefaultInputActionsController : MonoBehaviour
{
    private XRIDefaultInputActions _controls;
    private XRRayInteractor _rayInteractor;

    [SerializeField] GameObject leftRayController;
    [SerializeField] LocomotionManager _locomotionManager;

    private void Awake()
    {
        _rayInteractor = leftRayController.GetComponent<XRRayInteractor>();
        _controls = new XRIDefaultInputActions();

        _controls.XRILeftHand.Select.started += ctr => performSelection();
    }

    private void LateUpdate()
    {
        _rayInteractor.enabled = _controls.XRILeftHand.Activate.IsPressed();
        _locomotionManager.TriggerIsActive = _controls.XRILeftHand.Activate.IsPressed();

        _locomotionManager.WriteDebugMessage();
    }

    private void OnEnable()
    {
        _controls.XRILeftHand.Enable();
    }

    private void OnDisable()
    {
        _controls.XRILeftHand.Disable();
    }

    private void performSelection()
    {
        _locomotionManager.performSelection();
    }
}