using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FifteenGame : Singleton<FifteenGame>
{
    [SerializeField] private int _moves;
    [SerializeField] private float _seconds;
    [SerializeField] private GameBoard _gameBoard;

    public event Action<int> MovesChanged;
    public event Action<float> TimeChanged;
    
    public int Moves => _moves;

    public float Seconds => _seconds;

    public GameBoard Board
    {
        get => _gameBoard;
        set => _gameBoard = value;
    }


    public void IncreaseScore()
    {
        _moves++;
        MovesChanged?.Invoke(_moves);
    }
    
    /// <summary>
    /// Перезагрузка уровня
    /// </summary>
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void SaveGame()
    {
        int arrayWidth = _gameBoard.Pieces.GetLength(0);
        int arrayHeight = _gameBoard.Pieces.GetLength(1);
        
        PieceData[,] piecesData = new PieceData[arrayWidth, arrayHeight] ;
        for(int x = 0; x < arrayWidth; x++)
        for (int y = 0; y < arrayHeight; y++)
            piecesData[x, y] = _gameBoard.Pieces[x, y];
        
        
        GameDataProvider saver = new GameDataProvider();
        GameData data = new GameData()
        {
            Pieces = piecesData,
            ElapsedTime = Instance.Seconds,
            MovesCount = Instance.Moves
        };
        saver.Save(data);
    }

    public void LoadGame()
    {
        GameDataProvider reader = new GameDataProvider();
        GameData data = reader.Load();

        _gameBoard.Initialize(data.Pieces);
        _seconds = data.ElapsedTime;
        _moves = data.MovesCount;
        
        MovesChanged?.Invoke(_moves);
        TimeChanged?.Invoke(_seconds);
    }
    

    
    private void Start()
    {
        _gameBoard.Initialize(3);
    }

    private void Update() => IncreaseTime();

    private void IncreaseTime()
    {
        _seconds += Time.deltaTime;
        TimeChanged?.Invoke(_seconds);
    }
}
