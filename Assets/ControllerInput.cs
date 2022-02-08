using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControllerInput : MonoBehaviour
{
    private InputAction Rotate;

    private Vector3 move3DVector;
    private Vector3 rise3DVector;

    [SerializeField] private InteractionManager interactionManager;
    [SerializeField] private InputActionAsset controllerAsset;
    [SerializeField] private CharacterController controller;
    [SerializeField] private float speed;

    private void Awake()
    {
        var currentActionMap = controllerAsset.FindActionMap("Default");

        Rotate = currentActionMap.FindAction("Rotate");

        Rotate.started += OnRotatePerformed;
        Rotate.canceled += OnRotatePerformed;
        Rotate.Enable();
    }


    // Update is called once per frame
    void LateUpdate()
    {
        if (interactionManager.SelectedObject != null)
        {
            switch (interactionManager.GripIsActive)
            {
                case false:
                    interactionManager.SelectedObject.transform.Rotate(move3DVector * (Time.deltaTime * 100.0f));
                    break;
                case true:
                    interactionManager.SelectedObject.transform.localPosition..y += 1 * x * 10;
                    break;
            }
        }
    }

    private int x = 1;

    private void OnRotatePerformed(InputAction.CallbackContext ctx)
    {
        Vector2 movement = ctx.ReadValue<Vector2>();
        move3DVector = new Vector3(movement.x, 0, movement.y);
        //rise3DVector = new Vector3(0, movement.y, 0);

        if (movement.y > 0)
        {
            x = 1;
            //rise3DVector = interactionManager.SelectedObject.transform.localPosition.p;
        }
        else if (movement.y < 0)
        {
            x = -1;
            rise3DVector = interactionManager.SelectedObject.transform.up * -1;
        }
        else
        {
            x = 0;
            rise3DVector = new Vector3(0, 0, 0);
        }

        //Debug.Log(movement.y);
    }
}