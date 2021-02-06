using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FifteenGame : Singleton<FifteenGame>
{
    [SerializeField] private int _moves;
    [SerializeField] private float _seconds;
    [SerializeField] private GameBoard _gameBoard;
    [SerializeField] private GameMode _currentMode = GameMode.Mode3;

    
    
    public event Action<int> MovesChanged;
    public event Action<float> TimeChanged;
    
    
    
    /// <summary>
    /// Количество сделанных ходов
    /// </summary>
    public int Moves => _moves;

    /// <summary>
    /// Прошедшее время с начала текущей игры
    /// </summary>
    public float Seconds => _seconds;



    public void SetGameMode(int mode)
    {
        _currentMode = (GameMode) mode;
        Debug.Log($"Mode = {mode} | CurrentMode = {_currentMode}");
    }

    /// <summary>
    /// Запуск самой игры
    /// </summary>
    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    /// <summary>
    /// Перезагрузка и создание нового уровня
    /// </summary>
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    /// <summary>
    /// Сохранить текущую игру
    /// </summary>
    public void SaveGame()
    {
        int arrayWidth = _gameBoard.Pieces.GetLength(0);
        int arrayHeight = _gameBoard.Pieces.GetLength(1);
        
        PieceData[,] piecesData = new PieceData[arrayWidth, arrayHeight] ;
        for(int x = 0; x < arrayWidth; x++)
        for (int y = 0; y < arrayHeight; y++)
            piecesData[x, y] = _gameBoard.Pieces[x, y];
        
        
        GameDataProvider saver = new GameDataProvider("gamedata" + _currentMode);
        GameData data = new GameData()
        {
            Pieces = piecesData,
            ElapsedTime = Instance.Seconds,
            MovesCount = Instance.Moves
        };
        saver.Save(data);
    }
    
    /// <summary>
    /// Загрузка текущей игры или выбранного режима
    /// </summary>
    public void LoadGame()
    {
        GameDataProvider reader = new GameDataProvider("gamedata" + _currentMode);
        GameData data = reader.Load();

        _gameBoard.Initialize(data.Pieces);
        _seconds = data.ElapsedTime;
        _moves = data.MovesCount;
        
        MovesChanged?.Invoke(_moves);
        TimeChanged?.Invoke(_seconds);
    }


    private void Awake()
    {
        
        DontDestroyOnLoad(gameObject);
        SceneManager.activeSceneChanged += SceneManagerSceneChanged;
    }

    private void SceneManagerSceneChanged(Scene arg0, Scene arg1)
    {
        _moves = 0;
        _seconds = 0;

        // 1 - Игровая сцена
        if (arg1.buildIndex == 1)
        {
            _gameBoard = FindObjectOfType<GameBoard>();
            _gameBoard.PieceMoved += IncreaseScore;
            _gameBoard.Initialize(_currentMode);

            // События на кнопки
            var restartButton = GameObject.Find("UI_RestartButton").GetComponent<Button>();
            var saveButton = GameObject.Find("UI_SaveButton").GetComponent<Button>();
            var loadButton = GameObject.Find("UI_LoadButton").GetComponent<Button>();
            
            restartButton.onClick.AddListener(Restart);
            saveButton.onClick.AddListener(SaveGame);
            loadButton.onClick.AddListener(LoadGame);
        }
    }

    private void Update() => IncreaseTime();
    
    private void IncreaseScore()
    {
        _moves++;
        MovesChanged?.Invoke(_moves);
    }
    
    private void IncreaseTime()
    {
        _seconds += Time.deltaTime;
        TimeChanged?.Invoke(_seconds);
    }
}
