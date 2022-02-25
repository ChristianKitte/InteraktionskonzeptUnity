using UnityEngine;

/// <summary>
/// Steuert die Kamera zur Sicht von oben auf ein selektiertes Objekt
/// </summary>
public class TopViewCameraTracker : MonoBehaviour
{
    /// <summary>
    /// Eine Instanz vom Camera
    /// </summary>
    [SerializeField] private Camera Camera;

    /// <summary>
    /// Der Abstand zum selektierten Objekt auf der globalen Y Achse
    /// </summary>
    [SerializeField] private float abstandY = 10.0f;

    /// <summary>
    /// Eine Instanz von Light
    /// </summary>
    [SerializeField] private Light topLight;

    /// <summary>
    /// Wird beim Laden der Komponente ausgeführt
    /// </summary>
    private void Awake()
    {
        Camera = GetComponent<Camera>();
    }

    /// <summary>
    /// Wird einmal je Frame aufgerufen
    /// </summary>
    private void Update()
    {
        if (InteractionManager.Instance.SelectedObject != null)
        {
            Transform selectedTransform = InteractionManager.Instance.SelectedObject.transform;

            var position = selectedTransform.position;
            Camera.transform.position = new Vector3(
                position.x,
                position.y + Vector3.up.y * abstandY,
                position.z);

            Camera.transform.LookAt(position);
            Camera.transform.Rotate(0, 180, 0, Space.World);

            topLight.transform.position = Camera.transform.position;

            Camera.enabled = true;
            topLight.enabled = true;
        }
        else
        {
            Camera.enabled = false;
            topLight.enabled = false;
        }
    }
}