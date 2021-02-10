using System;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class PopupWindow : MonoBehaviour
{
    [SerializeField] private Transform _canvas;
    
    public Transform Parent
    {
        get => _canvas;
        set => _canvas = value;
    }


    public void Show(PopupContext context)
    {
        // Настройка фона и контейнера
        transform.SetParent(_canvas);
        transform.localScale = Vector3.one;
        var rect = GetComponent<RectTransform>();
        rect.offsetMin = Vector2.zero;
        rect.offsetMax = Vector2.zero;
        
        
        // Настройка самого окна
        var contextWindow = GetComponentInChildren<ContextWindow>();
        contextWindow.Initialize(context);
    }
    
}
