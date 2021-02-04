﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FifteenGame : Singleton<FifteenGame>
{
    [SerializeField] private Text _movesText;
    [SerializeField] private int _moves;
    
    [SerializeField] private Text _timeText;
    private float _seconds;
    [SerializeField] private GameBoard _gameBoard;

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
        _movesText.text = $"Ходов сделано: {_moves}";
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
        _movesText.text = $"Ходов сделано: {_moves}";
    }
    
    
    
    private void Awake()
    {
        _moves = 0;
        _gameBoard.Initialize();
    }

    private void Update()
    {
        DrawTime();
    }

    private void DrawTime()
    {
        _seconds += Time.deltaTime;
        TimeSpan elapsedTime = TimeSpan.FromSeconds(_seconds);
        _timeText.text = "Время: " +  elapsedTime.ToString("mm':'ss");
    }
}
