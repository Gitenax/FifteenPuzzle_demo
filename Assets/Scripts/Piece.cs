using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Debug = UnityEngine.Debug;

public class Piece : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private Point _pointPosition;
    [SerializeField] private Vector2 _vectorPosition;
    [SerializeField] private Text _textValue;
    [SerializeField] private int _value;
    [SerializeField] private PieceType _type = PieceType.PIECE;
    private RectTransform _rect;
    [SerializeField] private Image _sprite;

    private float _width = 170;
    private float _height = 170;

    public event Action PositionChanged;
    
    

    public float Width => _width;

    public float Height => _height;


    public Point PointPosition
    {
        get => _pointPosition;
        set
        {
            if (_pointPosition != value)
            {
                _pointPosition = value;
                OnPositionChanged();
            }
        }
    }

    public int Value
    {
        get => _value;
        set => _value = value;
    }

    public PieceType Type
    {
        get => _type;
        set => _type = value;
    }

    // Нужен для быстрого преобразования
    public static implicit operator PieceData(Piece piece)
    {
        return new PieceData()
        {
            PointPosition = piece.PointPosition, 
            Value = piece.Value,
            Type = (int)piece.Type
        };
    }


    public void Initialize(PieceData data)
    {
        Initialize(data.Value, data.PointPosition, (PieceType)data.Type);
    }
    
    public void Initialize(int value, Point pointPosition, PieceType type = PieceType.PIECE)
    {
        _value = value;
        _pointPosition = pointPosition;
        _type = type;
        
        _sprite = GetComponentInChildren<Image>();
        _textValue = GetComponentInChildren<Text>();
        _rect = GetComponent<RectTransform>();
        
        _textValue.text = Value.ToString();
        SetPositionFromPoint();
        if(_type == PieceType.HOLE)
            SetAsHole();
    }

    public void Refresh()
    {
        SetPositionFromPoint();
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        GameBoard.Instance.MovePiece(this);
    }
    
    public void SetAsHole()
    {
        _textValue.enabled = false;
        _sprite.enabled = false;
        _type = PieceType.HOLE;
    }

    public override string ToString()
    {
        return $"Piece #{Value} [{PointPosition.X}, {PointPosition.Y}]";
    }
    
    
    
    private void SetPositionFromPoint()
    {
        _vectorPosition = new Vector2(
            _pointPosition.X * _width, 
            _pointPosition.Y * _height * (-1));

        _rect.anchoredPosition = _vectorPosition;
    }

    private void OnPositionChanged()
    {
        // При смене поля "PointPosition" координаты будут пересчитыватся
        SetPositionFromPoint();
        PositionChanged?.Invoke();
    }
}