using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class ElapsedTimeDisplay : MonoBehaviour
{
    private FifteenGame _game;
    [SerializeField] private Text _elapsedTimeText;

    
    private void Start()
    {
        _game = FifteenGame.Instance;
        
        if(_elapsedTimeText == null)
            _elapsedTimeText = GetComponent<Text>();
        
        _game.TimeChanged += OnTimeChanged;
    }

    private void OnDisable()
    {
        _game.TimeChanged -= OnTimeChanged;
    }

    private void OnTimeChanged(float obj)
    {
        // obj - текущее время игры
        TimeSpan elapsedTime = TimeSpan.FromSeconds(obj);
        _elapsedTimeText.text = "Время: " +  elapsedTime.ToString("mm':'ss");
    }
}
