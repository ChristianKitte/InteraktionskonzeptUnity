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

        _controls.XRILeftHand.Select.started += ctx => { InteractionManager.Instance.StartSelection = true; };
    }

    private void Update()
    {
        _rayInteractor.enabled = _controls.XRILeftHand.Activate.IsPressed();

        Vector2 upDown = _controls.XRIRightHand.Move.ReadValue<Vector2>();
        upDown3DVector = new Vector3(0, upDown.y, 0);

        InteractionManager.Instance.LeftTriggerIsActive = _controls.XRILeftHand.Activate.IsPressed();
        InteractionManager.Instance.LeftGripIsActive = _controls.XRILeftHand.Select.IsPressed();
        InteractionManager.Instance.RightGripIsActive = _controls.XRIRightHand.Select.IsPressed();
    }

    private void LateUpdate()
    {
        if (InteractionManager.Instance.SelectedObject != null)
        {
            switch (InteractionManager.Instance.RightGripIsActive)
            {
                case false:
                    // do nothing at the moment
                    break;
                case true:
                    if (InteractionManager.Instance.SelectedObject != null)
                    {
                        InteractionManager.Instance.SelectedObject.transform.Translate(
                            upDown3DVector * (Time.deltaTime * 5),
                            Space.World);
                    }

                    break;
            }
        }
    }
}