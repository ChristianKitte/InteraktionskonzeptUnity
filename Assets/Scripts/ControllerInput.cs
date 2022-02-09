using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class ControllerInput : MonoBehaviour
{
    private InputAction Rotate;
    private InputAction Move;
    private InputAction UpDown;

    private Vector3 rotate3DVector;
    private Vector3 move3DVector;
    private Vector3 upDown3DVector;

    [SerializeField] private InputActionAsset controllerAsset;
    [SerializeField] GameObject leftController;

    //[SerializeField] private CharacterController controller;
    [SerializeField] private float speedMove;
    [SerializeField] private float speedRotate;
    [SerializeField] private float speedUpDown;

    private void Awake()
    {
        var currentActionMap = controllerAsset.FindActionMap("Default");

        Rotate = currentActionMap.FindAction("Rotate");
        Move = currentActionMap.FindAction("Move");
        UpDown = currentActionMap.FindAction("UpDown");

        Rotate.started += OnRotatePerformed; // ==> separate Achsen !
        Rotate.canceled += OnRotatePerformed;
        Rotate.Enable();

        Move.started += OnMovePerformed; // ==> separate Achsen !
        Move.canceled += OnMovePerformed;
        Move.Enable();

        UpDown.started += OnUpDownPerformed; // ==> separate Achsen !
        UpDown.canceled += OnUpDownPerformed;
        UpDown.Enable();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (InteractionManager.Instance.SelectedObject != null)
        {
            switch (InteractionManager.Instance.LeftGripIsActive)
            {
                case false:
                    InteractionManager.Instance.SelectedObject.transform.Rotate(
                        rotate3DVector * (Time.deltaTime * speedRotate),
                        Space.World);
                    break;
                case true:
                    InteractionManager.Instance.SelectedObject.transform.Translate(
                        move3DVector * (Time.deltaTime * speedMove),
                        Space.World);
                    break;
            }


        }
    }

    private void OnRotatePerformed(InputAction.CallbackContext ctx)
    {
        Vector2 rotation = ctx.ReadValue<Vector2>();
        rotate3DVector = new Vector3(rotation.x, 0, rotation.y);
    }

    private void OnMovePerformed(InputAction.CallbackContext ctx)
    {
        Vector2 movement = ctx.ReadValue<Vector2>();
        move3DVector = new Vector3(movement.y, 0, movement.x);
    }

    private void OnUpDownPerformed(InputAction.CallbackContext ctx)
    {
        Vector2 upDown = ctx.ReadValue<Vector2>();
        upDown3DVector = new Vector3(0, upDown.x, 0);
    }
}