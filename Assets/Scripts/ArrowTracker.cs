using UnityEngine;

/// <summary>
/// Richtet die Richtungsanzeige in jedem Frame neu aus
/// </summary>
public class ArrowTracker : MonoBehaviour
{
    /// <summary>
    /// Hält ein ein Game Object, dass als Vorwärts/Rechtszeiger in Z/X Richtung fungiert
    /// </summary>
    [SerializeField] private GameObject forwardArrowObject;

    /// <summary>
    /// Wird einmal je Frame nach Update aufgerufen und richtet den forwardArrowObject aus
    /// </summary>
    void Update()
    {
        Vector3 arrowPosition = forwardArrowObject.transform.position;

        Vector3 lookForwardVector = new Vector3(arrowPosition.x + 0.1f, arrowPosition.y, arrowPosition.z);
        forwardArrowObject.transform.LookAt(lookForwardVector);
        forwardArrowObject.transform.Rotate(0, 180, 90, Space.World);
    }
}