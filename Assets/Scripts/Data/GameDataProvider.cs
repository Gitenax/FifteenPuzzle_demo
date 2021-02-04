using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public sealed class GameDataProvider
{
    
    public GameData Load()
    {
        using (FileStream stream = File.OpenRead(Application.persistentDataPath + "/gamedata.dat"))
        { 
            return new BinaryFormatter().Deserialize(stream) as GameData;
        }
    }

    public void Save(GameData data)
    {
        using (FileStream stream = File.Create(Application.persistentDataPath + "/gamedata.dat"))
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
