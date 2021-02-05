using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public sealed class GameDataProvider
{
    private string _fileName;
    

    public GameDataProvider(string fileName) => _fileName = fileName;
    
    public GameData Load()
    {
        string path = Application.persistentDataPath + $"/{_fileName}.dat";
        if (File.Exists(path))
        {
#if UNITY_EDITOR
            Debug.Log("<color=green>Файл сохранения загружен!</color>");
#endif
            using (FileStream stream = File.OpenRead(path))
                return new BinaryFormatter().Deserialize(stream) as GameData;
        }
        else
        {
#if UNITY_EDITOR
            Debug.LogWarning("<color=red>Файл сохранения отсутствует!</color>");
            
#endif
        }
        return null;
    }

    public void Save(GameData data)
    {
        using (FileStream stream = File.Create(Application.persistentDataPath + $"/{_fileName}.dat"))
        {
            new BinaryFormatter().Serialize(stream, data);
#if UNITY_EDITOR
            Debug.Log("<color=green>Данные сохранены!</color>");
#endif
        }
    }

    public void Clear()
    {
        
    }

}
