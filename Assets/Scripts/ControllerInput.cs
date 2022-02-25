using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Wertet die Eingaben des linken Controllers aus und hält sie
/// </summary>
public class ControllerInput : MonoBehaviour
{
    /// <summary>
    /// Hält die Action für Rotate
    /// </summary>
    private InputAction _rotateSelectedHorizontal;

    /// <summary>
    /// Hält die Action für Move Forward
    /// </summary>
    private InputAction _moveSelectedForward;

    /// <summary>
    /// Hält die Action für Move Right
    /// </summary>
    private InputAction _moveSelectedRight;

    /// <summary>
    /// Hält die Action für Raise
    /// </summary>
    private InputAction _raiseSelectedUp;

    /// <summary>
    /// Aktueller Wert für das Anheben
    /// </summary>
    private Vector3 _upDown3DVector;

    /// <summary>
    /// Aktueller Wert für die Rotation
    /// </summary>
    private float _rotate; //nach Left (-1), nach Right (1)

    /// <summary>
    /// Aktueller Wert für die Vorwärtsbewegung
    /// </summary>
    private float _forward; //Backward (aka Down -1), Forward (aka Up 1)

    /// <summary>
    /// Aktueller Wert für die Rechtsbewegung
    /// </summary>
    private float _right; //Left (-1), Right (1)

    /// <summary>
    /// Das InputAsset des linken Controller
    /// </summary>
    [SerializeField] private InputActionAsset controllerAsset;

    /// <summary>
    /// Die Geschwindigkeit bei der Bewegung
    /// </summary>
    [SerializeField] private float speedMove = 1.0f;

    /// <summary>
    /// Die Geschwindigkeit beim Rotieren
    /// </summary>
    [SerializeField] private float speedRotate = 1.0f;

    /// <summary>
    /// Die Geschwindigkeit beim Steigen
    /// </summary>
    [SerializeField] private float speedRaise = 1.0f;

    /// <summary>
    /// Wird aufgerufen, wenn die Aktion zum Rotieren ausgeführt wurde 
    /// </summary>
    /// <param name="ctx">Eine Instanz von CallbackContext (Open XR)</param>
    private void OnRotateSelectedHorizontalPerformed(InputAction.CallbackContext ctx)
    {
        Vector2 value = ctx.ReadValue<Vector2>();
        _rotate = value.x;
    }

    /// <summary>
    /// Wird aufgerufen, wenn die Aktion für nach rechts bewegen ausgeführt wurde 
    /// </summary>
    /// <param name="ctx">Eine Instanz von CallbackContext (Open XR)</param>
    private void OnMoveSelectedRightPerformed(InputAction.CallbackContext ctx)
    {
        Vector2 value = ctx.ReadValue<Vector2>();
        _right = value.x;
    }

    /// <summary>
    /// Wird aufgerufen, wenn die Aktion für vorwärts bewegen ausgeführt wurde 
    /// </summary>
    /// <param name="ctx">Eine Instanz von CallbackContext (Open XR)</param>
    private void OnMoveSelectedForwardPerformed(InputAction.CallbackContext ctx)
    {
        Vector2 value = ctx.ReadValue<Vector2>();
        _forward = value.y;
    }

    /// <summary>
    /// Wird aufgerufen, wenn die Aktion zum Anheben ausgeführt wurde 
    /// </summary>
    /// <param name="ctx">Eine Instanz von CallbackContext (Open XR)</param>
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

    /// <summary>
    /// Wird beim Laden der Komponente ausgeführt
    /// </summary>
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

    /// <summary>
    /// Wird einmal je Frame nach Update aufgerufen
    /// </summary>
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
}