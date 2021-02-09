using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class GameDataReader
{
    private string _fileName;
    
    public GameDataReader(string fileName) => _fileName = fileName;
    
    public GameData Read()
    {
        string path = Application.persistentDataPath + $"/{_fileName}.dat";
        if (File.Exists(path))
        {
            using (FileStream stream = File.OpenRead(path))
            {
                return new BinaryFormatter().Deserialize(stream) as GameData;
#if UNITY_EDITOR
                Debug.Log("<color=green>Файл сохранения загружен!</color>");
#endif
            }
        }
        else
        {
#if UNITY_EDITOR
            Debug.LogWarning("<color=red>Файл сохранения отсутствует!</color>");
#endif
        }
        return null;
    }
}
