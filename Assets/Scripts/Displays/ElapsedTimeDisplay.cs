using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class ElapsedTimeDisplay : MonoBehaviour
{
    private Text _elapsedTimeText;

    private void Awake()
    {
        _elapsedTimeText = GetComponent<Text>();
        FifteenGame.Instance.TimeChanged += OnTimeChanged;
    }

    private void OnTimeChanged(float obj)
    {
        // obj - текущее время игры
        TimeSpan elapsedTime = TimeSpan.FromSeconds(obj);
        _elapsedTimeText.text = "Время: " +  elapsedTime.ToString("mm':'ss");
    }
    
}
