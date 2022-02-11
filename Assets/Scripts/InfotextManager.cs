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

        if (InteractionManager.Instance.SelectedObject != null)
        {
            _infoText =
                $"Name: {InteractionManager.Instance.SelectedObject.name}" +
                System.Environment.NewLine +
                $"Höhe: {InteractionManager.Instance.GroundDistanceString}";
        }

        _textMeshPro.SetText(_infoText);
        _textMeshPro.ForceMeshUpdate();
    }

    private void LateUpdate()
    {
        SetText();
    }
}