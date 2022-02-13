using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.XR.Interaction.Toolkit;

public class DefaultInputActionsController : MonoBehaviour
{
    private XRRayInteractor _rayInteractor;
    private XRIDefaultInputActions _controls;

    [SerializeField] GameObject leftController;

    private void OnEnable()
    {
        _controls.XRILeftHand.Enable();
        _controls.XRIRightHand.Enable();
    }

    private void OnDisable()
    {
        _controls.XRILeftHand.Disable();
        _controls.XRIRightHand.Disable();
    }

    private void Awake()
    {
        _rayInteractor = leftController.GetComponent<XRRayInteractor>();
        
        _controls = new XRIDefaultInputActions();
        _controls.XRILeftHand.Select.started += ctx => { InteractionManager.Instance.StartSelection = true; };
    }

    private void Update()
    {
        _rayInteractor.enabled = _controls.XRILeftHand.Activate.IsPressed();

        InteractionManager.Instance.LeftTriggerIsActive = _controls.XRILeftHand.Activate.IsPressed();
        InteractionManager.Instance.LeftGripIsActive = _controls.XRILeftHand.Select.IsPressed();
        InteractionManager.Instance.RightGripIsActive = _controls.XRIRightHand.Select.IsPressed();
    }
}