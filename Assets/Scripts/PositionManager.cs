using UnityEngine;

/// <summary>
/// Steuert das Positionslicht selektierter Objekte sowie dessen Lot zum Grund
/// </summary>
public class PositionManager : MonoBehaviour
{
    /// <summary>
    /// Eine Instanz einer Lichtquelle
    /// </summary>
    [SerializeField] private Light PositionLight;

    /// <summary>
    /// Eine Instanz eines Linien Renderer
    /// </summary>
    [SerializeField] private LineRenderer PositionLineRenderer;

    /// <summary>
    /// Die Distanz, ab dessen Unterschreitung eine Linie gezeichnet wird 
    /// </summary>
    [SerializeField] private float ActivateDistance = 2.5f;

    /// <summary>
    /// Wird kurz vor dem ersten Aufruf einer Update Methode aufgerufen
    /// </summary>
    private void Start()
    {
        PositionLineRenderer = GetComponent<LineRenderer>();
    }

    /// <summary>
    /// Wird einmal je Frame aufgerufen
    /// </summary>
    void Update()
    {
        PositionLineRenderer.enabled = false;
        PositionLight.enabled = false;

        InteractionManager.Instance.GroundDistanceString = "no ground";

        if (InteractionManager.Instance.SelectedObject != null)
        {
            Transform selectedObjectTransform = InteractionManager.Instance.SelectedObject.transform;

            PositionLight.enabled = true;
            PositionLight.transform.position = selectedObjectTransform.position;

            RaycastHit hitInfo;
            if (Physics.Raycast(selectedObjectTransform.position, Vector3.down, out hitInfo))
            {
                if (hitInfo.distance <= ActivateDistance)
                {
                    Vector3 _startRayVector3D = selectedObjectTransform.position;
                    Vector3 _endRayVector3D = new Vector3(_startRayVector3D.x, hitInfo.point.y,
                        _startRayVector3D.z);

                    PositionLineRenderer.SetPositions(new[] { _startRayVector3D, _endRayVector3D });
                    PositionLineRenderer.enabled = true;

                    InteractionManager.Instance.GroundDistance = hitInfo.distance;
                    InteractionManager.Instance.GroundDistanceString = hitInfo.distance.ToString();
                }
            }
        }
    }
}