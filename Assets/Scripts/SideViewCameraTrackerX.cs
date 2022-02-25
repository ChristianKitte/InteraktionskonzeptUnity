using UnityEngine;

/// <summary>
/// Steuert die Kamera zur Sicht von rechts auf ein selektiertes Objekt
/// </summary>
public class SideViewCameraTrackerX : MonoBehaviour
{
    /// <summary>
    /// Eine Instanz vom Camera
    /// </summary>
    [SerializeField] private Camera Camera;

    /// <summary>
    /// Der Abstand zum selektierten Objekt auf der globalen X Achse
    /// </summary>
    [SerializeField] private float abstandX = 10.0f;

    /// <summary>
    /// Die Höhe der Kamera relativ zum selektierten Objekt
    /// </summary>
    [SerializeField] private float offset = 1.0f;

    /// <summary>
    /// Die Rotation der Kamera um die X Achse relative zur Blickrichtung der Kamera
    /// </summary>
    [SerializeField] private float rotation = 3.0f;

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
    void Update()
    {
        if (InteractionManager.Instance.SelectedObject != null)
        {
            Transform selectedTransform = InteractionManager.Instance.SelectedObject.transform;

            var position = selectedTransform.position;
            Camera.transform.position = new Vector3(
                position.x + Vector3.right.x * abstandX,
                position.y + offset,
                position.z);

            Camera.transform.LookAt(position);
            Camera.transform.Rotate(rotation, 0, 180);

            Camera.enabled = true;
        }
        else
        {
            Camera.enabled = false;
        }
    }
}