using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SavePlayer(Player player)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/unicornpc.sav";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(player);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerData LoadPlayer()
    {
        string path = Application.persistentDataPath + "/unicornpc.sav";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.Log("File not found");
            return null;
        }
    }
    public static void SaveSets(Player player)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/unicornsets1.sav";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerSets data = new PlayerSets(player);

        formatter.Serialize(stream, data);
        stream.Close();
    }
    public static PlayerSets LoadSets()
    {
        string path = Application.persistentDataPath + "/unicornsets1.sav";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerSets data = formatter.Deserialize(stream) as PlayerSets;
            stream.Close();

            return data;
        }
        else
        {
            Debug.Log("File not found");
            return null;
        }
    }
}
