using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class ControllerInput : MonoBehaviour
{
    private InputAction _rotateSelectedHorizontal;
    private InputAction _moveSelectedForward;
    private InputAction _moveSelectedRight;
    private InputAction _raiseSelectedUp;

    private Vector3 _upDown3DVector;

    private float _rotate; //Left (-1), Right (1)
    private float _forward; //Backward (aka Down -1), Forward (aka Up 1) 
    private float _right; //Left (-1), Right (1)

    [SerializeField] private InputActionAsset controllerAsset;
    [SerializeField] private float speedMove = 1.0f;
    [SerializeField] private float speedRotate = 1.0f;
    [SerializeField] private float speedRaise = 1.0f;

    private void Awake()
    {
        var currentActionMap = controllerAsset.FindActionMap("Default");

        _rotateSelectedHorizontal = currentActionMap.FindAction("RotateHorizontal", true);
        _rotateSelectedHorizontal.performed += OnRotateSelectedHorizontalPerformed;
        _rotateSelectedHorizontal.Enable();

        _moveSelectedForward = currentActionMap.FindAction("MoveForward", true);
        _moveSelectedForward.performed += OnMoveSelectedForwardPerformed;
        _moveSelectedForward.Enable();

        _moveSelectedRight = currentActionMap.FindAction("MoveRight", true);
        _moveSelectedRight.performed += OnMoveSelectedRightPerformed;
        _moveSelectedRight.Enable();

        _raiseSelectedUp = currentActionMap.FindAction("RaiseUp", true);
        _raiseSelectedUp.performed += OnRaiseSelectedUpPerformed;
        _raiseSelectedUp.Enable();
    }

    void LateUpdate()
    {
        if (InteractionManager.Instance.SelectedObject != null)
        {
            switch (InteractionManager.Instance.LeftGripIsActive)
            {
                case true:
                    Vector3 rotate3DVector = new Vector3(0, _rotate, 0);

                    if (_upDown3DVector != Vector3.zero)
                    {
                        InteractionManager.Instance.SelectedObject.transform.Translate(
                            _upDown3DVector * (speedRaise * Time.deltaTime),
                            Space.World);

                        _upDown3DVector = Vector3.zero;
                    }

                    InteractionManager.Instance.SelectedObject.transform.Rotate(
                        rotate3DVector * (speedRotate * Time.deltaTime),
                        Space.World);

                    break;

                case false:
                    Vector3 move3DVector = new Vector3(_right, 0, _forward);

                    InteractionManager.Instance.SelectedObject.transform.Translate(
                        move3DVector * (speedMove * Time.deltaTime),
                        Space.World);

                    break;
            }
        }
    }

    private void OnRotateSelectedHorizontalPerformed(InputAction.CallbackContext ctx)
    {
        Vector2 value = ctx.ReadValue<Vector2>();
        _rotate = value.x;
    }

    private void OnMoveSelectedRightPerformed(InputAction.CallbackContext ctx)
    {
        Vector2 value = ctx.ReadValue<Vector2>();
        _right = value.x;
    }

    private void OnMoveSelectedForwardPerformed(InputAction.CallbackContext ctx)
    {
        Vector2 value = ctx.ReadValue<Vector2>();
        _forward = value.y;
    }

    private void OnRaiseSelectedUpPerformed(InputAction.CallbackContext ctx)
    {
        Vector2 value = ctx.ReadValue<Vector2>();

        if (InteractionManager.Instance.SelectedObject != null)
        {
            RaycastHit hitInfo;
            if (Physics.Raycast(
                    InteractionManager.Instance.SelectedObject.transform.position,
                    Vector3.down,
                    out hitInfo))
            {
                if (hitInfo.distance <= 0)
                {
                    _upDown3DVector = Vector3.up;
                }
                else
                {
                    _upDown3DVector = new Vector3(0, value.y, 0);
                }
            }
            else
            {
                _upDown3DVector = new Vector3(0, value.y, 0);
            }
        }
    }
}