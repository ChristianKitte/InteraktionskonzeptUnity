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
    private XRRayInteractor _rayInteractor;
    private XRIDefaultInputActions _controls;
    private Vector3 upDown3DVector;

    [SerializeField] GameObject leftController;

    [SerializeField] GameObject rightController;

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

        _controls.XRILeftHand.Select.started += ctx => selectionStarted();
    }

    private void Update()
    {
        _rayInteractor.enabled = _controls.XRILeftHand.Activate.IsPressed();

        upDown = _controls.XRIRightHand.Move.ReadValue<Vector2>();
        upDown3DVector = new Vector3(0, upDown.y, 0);
        //Debug.Log(x.ToString());

        InteractionManager.Instance.LeftTriggerIsActive = _controls.XRILeftHand.Activate.IsPressed();
        InteractionManager.Instance.LeftGripIsActive = _controls.XRILeftHand.Select.IsPressed();
        InteractionManager.Instance.RightGripIsActive = _controls.XRIRightHand.Select.IsPressed();
    }

    private Vector2 upDown;

    private void LateUpdate()
    {
        switch (InteractionManager.Instance.RightGripIsActive)
        {
            case false:
                // do nothing at the moment
                break;
            case true:
                //Vector2 upDown = ctx.ReadValue<Vector2>();
                //Vector3 upDown3DVector = new Vector3(0, upDown.y, 0);

                InteractionManager.Instance.SelectedObject.transform.Translate(
                    upDown3DVector * (Time.deltaTime * 5),
                    Space.World);

                //Debug.Log(x.ToString());
                break;
        }
    }

    private void selectionStarted()
    {
        InteractionManager.Instance.StartSelection = true;
    }
}