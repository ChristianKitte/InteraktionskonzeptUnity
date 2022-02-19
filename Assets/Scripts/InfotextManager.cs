using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;

public class InfotextManager : MonoBehaviour
{
    private TextMeshPro _textMeshPro;

    private void Awake()
    {
        _textMeshPro = GetComponent<TextMeshPro>();
    }

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

        _textMeshPro.SetText(_infoText,true);
        _textMeshPro.color = _ionfoColor;

        _textMeshPro.ForceMeshUpdate();
    }

    private void LateUpdate()
    {
        SetText();
    }
}