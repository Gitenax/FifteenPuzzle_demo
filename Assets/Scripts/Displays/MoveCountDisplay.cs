using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class MoveCountDisplay : MonoBehaviour
{
    private FifteenGame _game = FifteenGame.Instance;
    [SerializeField] private Text _moveCountText;

    
    private void OnEnable()
    {
        if(_moveCountText == null)
            _moveCountText = GetComponent<Text>();
        
        _game.MovesChanged += OnMovesChanged;
    }

    private void OnDisable()
    {
        _game.MovesChanged -= OnMovesChanged;
    }

    private void OnMovesChanged(int obj)
    {
        // obj - текущее количество ходов
        _moveCountText.text = $"Ходов сделано: {obj}";
    }
}
