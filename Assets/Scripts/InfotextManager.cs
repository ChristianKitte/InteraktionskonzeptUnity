using System;
using TMPro;
using UnityEngine;

/// <summary>
/// Steuert die Anzeige von Objekthöhe und Objektname
/// </summary>
public class InfotextManager : MonoBehaviour
{
    /// <summary>
    /// Eine Instanz von TextMeshPro
    /// </summary>
    private TextMeshPro _textMeshPro;

    /// <summary>
    /// Setzt den aktuellen Text an Hand der Werte aus dem InteractionManager 
    /// </summary>
    private void SetText()
    {
        String _infoText = "Kein aktives Objekt";
        Color _ionfoColor = Color.green;

        if (InteractionManager.Instance.CurrentObject != null && InteractionManager.Instance.SelectedObject == null)
        {
            _infoText =
                $"Name: {InteractionManager.Instance.CurrentObject.name}" +
                System.Environment.NewLine;

            _ionfoColor = Color.red;
        }
        else if (InteractionManager.Instance.SelectedObject != null)
        {
            _infoText =
                $"Name: {InteractionManager.Instance.SelectedObject.name}" +
                System.Environment.NewLine +
                $"Höhe: {InteractionManager.Instance.GroundDistanceString}";

            _ionfoColor = Color.green;
        }
        else if (InteractionManager.Instance.CurrentObject == null &&
                 InteractionManager.Instance.SelectedObject == null)
        {
            _infoText = "";
        }

        _textMeshPro.SetText(_infoText, true);
        _textMeshPro.color = _ionfoColor;

        _textMeshPro.ForceMeshUpdate();
    }

    /// <summary>
    /// Wird beim Laden der Komponente ausgeführt
    /// </summary>
    private void Awake()
    {
        _textMeshPro = GetComponent<TextMeshPro>();
    }

    /// <summary>
    /// Wird einmal je Frame nach Update aufgerufen
    /// </summary>
    private void LateUpdate()
    {
        SetText();
    }
}