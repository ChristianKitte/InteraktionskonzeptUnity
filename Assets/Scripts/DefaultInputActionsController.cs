using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

/// <summary>
/// Einzige Aufgabe ist die Aktivierung des Strahls zur Fokussierung von Objekten, sofern die Grip Taste (Select)
/// gepresst ist
/// </summary>
public class DefaultInputActionsController : MonoBehaviour
{
    /// <summary>
    /// Eine Instanz des XRRayInteractor (Strahl zur Fokussierung von Objekten)
    /// </summary>
    private XRRayInteractor _rayInteractor;

    /// <summary>
    /// Hält eine Instanz auf das Sample Asset des OpenInput Asset
    /// </summary>
    private XRIDefaultInputActions _controls;

    /// <summary>
    /// Hält eine Referenz auf den linken Controller
    /// </summary>
    [SerializeField] GameObject leftController;

    /// <summary>
    /// Wird beim Laden der Komponente ausgeführt
    /// </summary>
    private void OnEnable()
    {
        _controls.XRILeftHand.Enable();
        _controls.XRIRightHand.Enable();
    }

    /// <summary>
    /// Wird beim Entladen der Komponente ausgeführt
    /// </summary>
    private void OnDisable()
    {
        _controls.XRILeftHand.Disable();
        _controls.XRIRightHand.Disable();
    }

    /// <summary>
    /// Wird beim Laden der Komponente ausgeführt
    /// </summary>
    private void Awake()
    {
        _rayInteractor = leftController.GetComponent<XRRayInteractor>();

        _controls = new XRIDefaultInputActions();
        _controls.XRILeftHand.Select.performed += ctx => { InteractionManager.Instance.StartSelection = true; };
    }

    /// <summary>
    /// Wird einmal je Frame aufgerufen
    /// </summary>
    private void Update()
    {
        _rayInteractor.enabled = _controls.XRILeftHand.Activate.IsPressed();

        InteractionManager.Instance.LeftTriggerIsActive = _controls.XRILeftHand.Activate.IsPressed();
        InteractionManager.Instance.LeftGripIsActive = _controls.XRILeftHand.Select.IsPressed();
        InteractionManager.Instance.RightGripIsActive = _controls.XRIRightHand.Select.IsPressed();
    }
}