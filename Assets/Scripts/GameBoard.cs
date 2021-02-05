using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameBoard : Singleton<GameBoard>
{
    private static System.Random random;
    
    [SerializeField] private Piece _pieceGameObject;
    [SerializeField] private RectTransform _gameBoard;
    
    // Размерность
    [SerializeField] private int _width = 4;
    [SerializeField] private int _height = 4;
    
    // Логическая массив элементов
    private Piece[,] _pieces;

    public Piece[,] Pieces
    {
        get => _pieces;
        set
        {
            _pieces = value;
            foreach (var piece in _pieces)
                piece.Refresh();
        }
    }


    /// <summary>
    /// Инициализация новой игровой области
    /// </summary>
    /// <param name="scale">Размерность игровой доски</param>
    public void Initialize(int scale)
    {
        _width = _height = scale;
        InitializePieces();
    }

    /// <summary>
    /// Инициализация массива игровых ячеек на готовой игровой области
    /// </summary>
    /// <param name="data">Массив данных ячеек</param>
    public void Initialize(PieceData[,] data)
    {
        ClearBoard();

        _width = data.GetLength(0);
        _height = data.GetLength(1);
        _pieces = new Piece[_width, _height];
        
        
        // Заполнение
        for(int y = 0; y < _pieces.GetLength(0); y++)
        for (int x = 0; x < _pieces.GetLength(1); x++)
        {
            var piece = Instantiate(_pieceGameObject, _gameBoard);
            piece.Initialize(data[x, y]);
            piece.name = piece.ToString();

            _pieces[x, y] = piece;
        }
        
        // Подписка на события
        for(int x = 0; x < _pieces.GetLength(0); x++)
        for (int y = 0; y < _pieces.GetLength(1); y++)
        {
            _pieces[x, y].PositionChanged += OnPiecePositionChanged;
           
            if (_pieces[x, y].Type == Piece.PieceType.HOLE)
                _pieces[x, y].PositionChanged -= OnPiecePositionChanged;
        }
        
        // Размер доски
        SetBoradSize();
    }
    
    
    private void InitializePieces()
    {
        random = new System.Random();
        _pieces = new Piece[_width, _height];
        
        // Заполнение
        int currentPiece = 0;
        for(int y = 0; y < _pieces.GetLength(0); y++)
        for (int x = 0; x < _pieces.GetLength(1); x++)
        {
            currentPiece++;
            var piece = Instantiate(_pieceGameObject, _gameBoard);
            piece.Initialize(currentPiece, new Point(x, y));
            piece.name = piece.ToString();

            _pieces[x, y] = piece;
        }

        // Перемешивание
        do
        {
            Shuffle();
            _pieces[_width - 1, _height - 1].SetAsHole();
        } while (!CheckResolveCondition());
        
        // Подписка на события
        for(int x = 0; x < _pieces.GetLength(0); x++)
        for (int y = 0; y < _pieces.GetLength(1); y++)
            _pieces[x, y].PositionChanged += OnPiecePositionChanged;
        
        _pieces[_width - 1, _height - 1].PositionChanged -= OnPiecePositionChanged;
        
        // Размер доски
        SetBoradSize();
    }
    
    
    
    public void MovePiece(Piece piece)
    {
        Piece hole = null;
        Point[] directions = {Point.Left, -Point.Up, Point.Right, -Point.Down};
        
        // Проверка по 4-м направлениям
        for (int i = 0; i < 4; i++)
        {
            Stack<Piece> checkingPieces = new Stack<Piece>();
            checkingPieces.Push(piece);

            // Сбор всех ячеек в конкретном направлении
            while (true)
            {
                var nextPiece = GetPieceAtPoint(piece.PointPosition + directions[i] * checkingPieces.Count);
                if (nextPiece != null)
                    if (nextPiece.Type != Piece.PieceType.HOLE)
                        checkingPieces.Push(nextPiece);
                    else
                    {
                        hole = nextPiece;
                        break;
                    }
                else
                    break;
            }
            
            //Смещение ячеек
            if (hole != null)
            {
                foreach (var currentPiece in checkingPieces)
                    FlipPieces(hole, currentPiece);

                CheckWinCondition();
                break;
            }
        }
    }

    
    // Инициализация
    private void Awake()
    {
        if (_gameBoard == null)
            _gameBoard = FindObjectOfType<GameBoard>().GetComponent<RectTransform>();
    }
    
    private void OnPiecePositionChanged()
    {
        FifteenGame.Instance.IncreaseScore();
    }

    private Piece GetPieceAtPoint(Point point)
    {
        if((point.X >= 0 && point.Y >= 0) 
           && (point.X < _width && point.Y < _height))
            return _pieces[point.X, point.Y];

        // Если "point" за пределами массива
        return null;
    }
    
    private void FlipPieces(Piece pieceOne, Piece pieceTwo)
    {
        Piece tempPiece = pieceOne;
        Point tempPoint = pieceOne.PointPosition;

        // Замена в массиве
        _pieces[pieceOne.PointPosition.X, pieceOne.PointPosition.Y] = pieceTwo;
        _pieces[pieceTwo.PointPosition.X, pieceTwo.PointPosition.Y] = tempPiece;
        
        pieceOne.PointPosition = pieceTwo.PointPosition;
        pieceTwo.PointPosition = tempPoint;
    }

    private void Shuffle()
    {
        int lastX = _width - 1;
        int lastY = _height - 1;
        
        Piece lastPiece = _pieces[lastX, lastY];
        Point lastPiecePoint = _pieces[lastX, lastY].PointPosition;
        
        int a = _pieces.GetLength(0);
        int b = _pieces.GetLength(1);

        for(int x = 0; x < a; x++)
        for (int y = 0; y < b; y++)
        {
            var temp = _pieces[x, y];
            int newX = random.Next(a - x);
            int newY = random.Next(b - y);
            
            _pieces[x, y] = _pieces[newX, newY];
            _pieces[newX, newY] = temp;
        }
        
        for(int x = 0; x < _pieces.GetLength(0); x++)
        for (int y = 0; y < _pieces.GetLength(1); y++)
            _pieces[x, y].PointPosition = new Point(x, y);
        
        
        // Возвращение последней ячейки(пустой) на свое место
        _pieces[lastPiece.PointPosition.X, lastPiece.PointPosition.Y] = _pieces[_width - 1, _height - 1];
        _pieces[lastX, lastY] = lastPiece;
        
        _pieces[lastPiece.PointPosition.X, lastPiece.PointPosition.Y].PointPosition = new Point(lastPiece.PointPosition.X, lastPiece.PointPosition.Y);
        _pieces[lastX, lastY].PointPosition = lastPiecePoint;
    }

    private void ClearBoard()
    {
        if(_pieces == null) return;

        foreach (var piece in _pieces)
        {
            piece.PositionChanged -= OnPiecePositionChanged;
            Destroy(piece.gameObject);
        } 
        
        Array.Clear(_pieces, 0, _pieces.Length);
    }
    
    private void SetBoradSize()
    {
        if(_pieces[0, 0] == null)
            return;
        
        var piece = _pieces[0, 0];
        float offset = 10;

        _gameBoard.sizeDelta = new Vector2(
            _width * piece.Width + offset, 
            _height * piece.Height + offset);
    }
    
    private void CheckWinCondition()
    {
        int currentValue = 0;
        bool isWin = true;
        
        for(int y = 0; y < _pieces.GetLength(0); y++)
        for (int x = 0; x < _pieces.GetLength(1); x++)
        {
            currentValue++;
            if (_pieces[x, y].Value != currentValue)
            {
                isWin = false;
                break;
            }
        }
        
        if(isWin)
            Debug.Log("<color=green>YOU WIN</color>");
    }

    private bool CheckResolveCondition()
    {
        // Одномерный массив для удобства суммирования
        Piece[] tempPieces = new Piece[_width * _height];
        
        int currentIndex = 0;
        for(int y = 0; y < _pieces.GetLength(0); y++)
        for (int x = 0; x < _pieces.GetLength(1); x++)
        {
            tempPieces[currentIndex] = _pieces[x, y];
            currentIndex++;
        }
        
        int valueSum = 0;
        for (int i = 0; i < tempPieces.Length; i++)
        {
            var currentPiece = tempPieces[i];
            int valueCount = 0;
            
            for (int j = i; j < tempPieces.Length; j++)
            {
                if (tempPieces[j].Value < currentPiece.Value)
                    valueCount++;
            }

            valueSum += valueCount;
        }

#if UNITY_EDITOR
        if(valueSum % 2 == 0)
            Debug.Log("<color=green>РАЗРЕШИМО</color>");
        else
            Debug.Log("<color=red>НЕ РАЗРЕШИМО</color>");
#endif
        
        return valueSum % 2 == 0;
    }
}