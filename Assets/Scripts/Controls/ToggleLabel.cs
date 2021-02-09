using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle)), ExecuteAlways]
public class ToggleLabel : MonoBehaviour
{
    [SerializeField] private Text _label;
    [SerializeField] private Toggle _toggle;
    
    private void OnEnable()
    {
        _toggle = GetComponent<Toggle>();
        _toggle.onValueChanged.AddListener(OnChangedValue);
    }

    private void OnDisable()
    {
        _toggle.onValueChanged.RemoveListener(OnChangedValue);
    }

    private void OnChangedValue(bool state)
    {
        if(state)
            _label.color = Color.black;
        else
            _label.color = Color.white;
    }
}
