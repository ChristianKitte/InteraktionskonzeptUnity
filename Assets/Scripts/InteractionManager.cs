using UnityEngine;

/// <summary>
/// Steuert das Zusammenspiel aller Komponenten und hält den jeweils aktuellen Zustand
/// </summary>
public class InteractionManager : MonoBehaviour
{
    [SerializeField] private Camera infoCanvasCamera;

    /// <summary>
    /// Das aktuell selektierte Objekt
    /// </summary>
    private GameObject _selectedObject = null;

    /// <summary>
    /// Das aktuell aktive Objekt
    /// </summary>
    private GameObject _currentObject = null;

    /// <summary>
    /// Gibt an, ob der Trigger des linken Controller gedrückt ist
    /// </summary>
    private bool _leftLeftTriggerIsActive = false;

    /// <summary>
    /// Gibt an, ob die Grip Taste des linken Controller gedrückt ist
    /// </summary>
    private bool _leftGripIsActive = false;

    /// <summary>
    /// Gibt an, ob der Trigger des rechten Controller gedrückt ist
    /// </summary>
    private bool _rightGripIsActive = false;

    /// <summary>
    /// Gibt an, ob eine Selektion gestartet wurde (aktiv ist)
    /// </summary>
    private bool _startSelection = false;

    /// <summary>
    /// Die einzoge Instanz des Objektes
    /// </summary>
    public static InteractionManager Instance { get; private set; }

    /// <summary>
    /// Legt das aktuell selektierte Objekt fest oder gibt es zurück
    /// </summary>
    public GameObject SelectedObject
    {
        get => _selectedObject;
        set => _selectedObject = value;
    }

    /// <summary>
    /// Legt das aktuell aktive Objekt fest oder gibt es zurück
    /// </summary>
    public GameObject CurrentObject
    {
        get => _currentObject;
        set => _currentObject = value;
    }

    /// <summary>
    /// Legt fest oder gibt zurück, ob der Trigger des linken Controllers gedrückt ist
    /// </summary>
    public bool LeftTriggerIsActive
    {
        get => _leftLeftTriggerIsActive;
        set => _leftLeftTriggerIsActive = value;
    }

    /// <summary>
    /// Legt fest oder gibt zurück, ob die Grip Taste des linken Controllers gedrückt ist
    /// </summary>
    public bool LeftGripIsActive
    {
        get => _leftGripIsActive;
        set => _leftGripIsActive = value;
    }

    /// <summary>
    /// Legt fest oder gibt zurück, ob die Grip Taste des rechten Controllers gedrückt ist
    /// </summary>
    public bool RightGripIsActive
    {
        get => _rightGripIsActive;
        set => _rightGripIsActive = value;
    }

    /// <summary>
    /// Legt fest oder gibt zurück, ob eine Selektion gestartet wurde (aktiv ist)
    /// </summary>
    public bool StartSelection
    {
        get => _startSelection;
        set => _startSelection = value;
    }

    /// <summary>
    /// Legt fest oder gibt die aktuelle Distanz zum definierten Grund zurück 
    /// </summary>
    public float GroundDistance { get; set; }

    /// <summary>
    /// Legt fest oder gibt die aktuelle Distanz zum definierten Grund als String zurück
    /// </summary>
    public string GroundDistanceString { get; set; }

    /// <summary>
    /// Wird beim Laden der Komponente ausgeführt 
    /// </summary>
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }

        Instance = this;
        infoCanvasCamera.enabled = false;
    }

    /// <summary>
    /// Wird einmal je Frame nach Update aufgerufen
    /// </summary>
    private void LateUpdate()
    {
        if (_startSelection && _leftLeftTriggerIsActive)
        {
            if (_currentObject == null) // fertig
            {
                _startSelection = false;
            }
            else if (_currentObject != null)
            {
                if (_selectedObject == null) // selektieren
                {
                    _selectedObject = _currentObject;
                    _selectedObject.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
                    _selectedObject.GetComponent<Rigidbody>().isKinematic = true;

                    _startSelection = false;
                    _currentObject = null;

                    if (infoCanvasCamera != null)
                    {
                        infoCanvasCamera.enabled = true;
                    }
                }
                else if (_selectedObject.Equals(_currentObject)) // deselektieren
                {
                    _selectedObject.GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
                    _selectedObject.GetComponent<Rigidbody>().isKinematic = false;
                    _selectedObject = null;

                    _startSelection = false;
                    _currentObject = null;

                    if (infoCanvasCamera != null)
                    {
                        infoCanvasCamera.enabled = false;
                    }
                }
                else if (!_selectedObject.Equals(_currentObject)) // deselektieren und selektieren
                {
                    _selectedObject.GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
                    _selectedObject.GetComponent<Rigidbody>().isKinematic = false;
                    _selectedObject = null;

                    _selectedObject = _currentObject;
                    _selectedObject.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
                    _selectedObject.GetComponent<Rigidbody>().isKinematic = true;

                    _startSelection = false;
                    _currentObject = null;

                    if (infoCanvasCamera != null)
                    {
                        infoCanvasCamera.enabled = true;
                    }
                }
            }
        }
    }

    /// <summary>
    /// Gibt einen Debugstring in die Konsole aus
    /// </summary>
    public void WriteDebugMessage()
    {
        if (_currentObject != null)
        {
            Debug.Log("Active object is: " + _currentObject.name);
        }
        else
        {
            Debug.Log("No active object");
        }

        if (_selectedObject != null)
        {
            Debug.Log("Selected object is: " + _selectedObject.name);
        }
        else
        {
            Debug.Log("No selecetd object");
        }

        Debug.Log("Trigger is: " + _leftLeftTriggerIsActive.ToString());
        Debug.Log("Trigger is: " + _startSelection.ToString());
    }
}