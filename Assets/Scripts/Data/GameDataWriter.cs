using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class GameDataWriter
{
    private string _fileName;
    
    public GameDataWriter(string fileName) => _fileName = fileName;
    
    public void Write(GameData data)
    {
        using (FileStream stream = File.Create(Application.persistentDataPath + $"/{_fileName}.dat"))
        {
            new BinaryFormatter().Serialize(stream, data);
#if UNITY_EDITOR
            Debug.Log("<color=green>Данные сохранены!</color>");
#endif
        }
    }
}
