using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class MoveCountDisplay : MonoBehaviour
{
    private Text _moveCountText;

    private void Awake()
    {
        _moveCountText = GetComponent<Text>();
        FifteenGame.Instance.MovesChanged += OnMovesChanged;
    }

    private void OnMovesChanged(int obj)
    {
        // obj - текущее количество ходов
        _moveCountText.text = $"Ходов сделано: {obj}";
    }
}
