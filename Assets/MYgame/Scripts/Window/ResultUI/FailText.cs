using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FailText : MonoBehaviour
{
    Color OrlColor;
    [SerializeField] TextMeshProUGUI textMesh;

    private void OnEnable()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
        OrlColor = textMesh.color;
        AddForceEffect();
    }
    private void OnDisable()
    {
        textMesh.color = OrlColor;
    }

    void AddForceEffect()
    {
        textMesh.color = Color.red;
        textMesh.fontStyle = FontStyles.Bold;
    }

}
