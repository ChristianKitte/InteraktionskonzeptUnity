using UnityEngine;

/// <summary>
/// Sucht in Vorwärtsrichtung nach einen selektierbaren Objekt, übergibt es dem InteractionManager und schaltet
/// EMISSION bei Erhalt des Fokus
/// </summary>
public class SelectionRayCast : MonoBehaviour
{
    /// <summary>
    /// Das Transformobjekt, das die Position enthält, aus welchem der Suchstrahl emittieren soll
    /// </summary>
    [SerializeField] private Transform startRay;

    /// <summary>
    /// Die Länge des Strahls, welcher zur Detektierung selektierbarer Objekten verwendet wird
    /// </summary>
    [SerializeField] private float rayLength = 100f;

    /// <summary>
    /// Hält einen String, der die Gruppe selektierbarer Objekte definiert
    /// </summary>
    [SerializeField] private string selectionTag;

    private GameObject _currentObject;
    private Renderer _currentRenderer;

    /// <summary>
    /// Wird einmal je Frame aufgerufen
    /// </summary>
    void Update()
    {
        if (InteractionManager.Instance.LeftTriggerIsActive && _currentObject != null && _currentRenderer != null)
        {
            _currentObject.GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
        }
    }

    /// <summary>
    /// Wird einmal je Frame nach Update aufgerufen
    /// </summary>
    private void LateUpdate()
    {
        if (InteractionManager.Instance.LeftTriggerIsActive && Physics.Raycast(
                startRay.transform.position,
                startRay.transform.forward,
                out var hit,
                rayLength
            ))
        {
            _currentObject = hit.transform.gameObject;

            if (_currentObject != null)
            {
                _currentRenderer = _currentObject.GetComponent<Renderer>();

                if (_currentRenderer != null && _currentObject.CompareTag(selectionTag))
                {
                    _currentObject.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
                    InteractionManager.Instance.CurrentObject = _currentObject;
                }
            }
        }
        else
        {
            _currentObject = null;
        }
    }
}