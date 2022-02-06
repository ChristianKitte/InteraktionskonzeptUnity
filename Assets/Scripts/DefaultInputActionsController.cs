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

    [SerializeField] GameObject leftController;
    [SerializeField] GameObject rightController;
    [SerializeField] InteractionManager interactionManager;

    private void Awake()
    {
        _rayInteractor = leftController.GetComponent<XRRayInteractor>();
        _controls = new XRIDefaultInputActions();

        _controls.XRILeftHand.Select.performed += ctr => performSelection();
    }

    private void LateUpdate()
    {
        _rayInteractor.enabled = _controls.XRILeftHand.Activate.IsPressed();

        interactionManager.TriggerIsActive = _controls.XRILeftHand.Activate.IsPressed();
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
        interactionManager.performSelection();
    }
}