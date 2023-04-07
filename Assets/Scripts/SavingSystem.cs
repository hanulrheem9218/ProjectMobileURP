using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class InternalSaving<MonoData>
{
    //Refactoring code.
    // object class dosent return null. you must return with default.
    public MonoData IdentifyObjectType(MonoData monoData)
    {
        switch (monoData.GetType().Name)
        {
            case "PlayerScript":
                {
                    MonoData playerMonoData = (MonoData)(object)monoData;
                    return playerMonoData;
                }
            default:
                {
                    // use deafult in generic.
                    return default;
                }
        }
    }
}
public static class SavingSystem
{
    private static BinaryFormatter formatter;
    private static FileStream stream;
    public static string GetCurrentPath<MonoData>()
    {
        string path = Application.persistentDataPath;
        string dataPath = "monoData";
        if (typeof(MonoData) == typeof(PlayerGeneralSystem))
        {
            //Debug.Log("wtf");
            //  TransferData playerObject = (TransferData)(object)monoData;
            // Debug.Log(path += "/" + playerObject.transform.name + dataPath + ".mono");
            return (path + "/" + "playerGeneralSystem" + dataPath + ".mono");

        }
        else if (typeof(MonoData) == typeof(EnemyScript))
        {
            return (path + "/" + "enemyData" + dataPath + ".mono");
        }

        return null;

    }

    public static void SaveData<MonoData>(object monoData)
    {
        Debug.Log(GetCurrentPath<MonoData>());
        stream = new FileStream(GetCurrentPath<MonoData>(), FileMode.Create);
        formatter = new BinaryFormatter();
        if (monoData.GetType() == typeof(PlayerGeneralSystem))
        {
            TransferData<PlayerGeneralSystem> playerData = new TransferData<PlayerGeneralSystem>().Instance((PlayerGeneralSystem)monoData);
            // Debug.Log(playerData.positions[0] + " : " + playerData.positions[1] + " : " + playerData.positions[2]);
            formatter.Serialize(stream, playerData);
            stream.Close();
        }
        else if (monoData.GetType() == typeof(EnemyScript))
        {
            TransferData<EnemyScript> enenmyData = new TransferData<EnemyScript>().Instance((EnemyScript)monoData);
            //  Debug.Log(playerData.positions[0] + " : " + playerData.positions[1] + " : " + playerData.positions[2]);
            formatter.Serialize(stream, enenmyData);
            stream.Close();
        }

        return;
    }

    public static object LoadData<MonoType, MonoData>()
    {
        return LoadSystemFile<MonoType, MonoData>();
    }
    public static object LoadSystemFile<MonoType, MonoData>()
    {
        Debug.Log(GetCurrentPath<MonoData>());
        if (File.Exists(GetCurrentPath<MonoData>()))
        {
            formatter = new BinaryFormatter();
            stream = new FileStream(GetCurrentPath<MonoData>(), FileMode.Open);
            MonoType data = (MonoType)(object)formatter.Deserialize(stream);
            stream.Close();
            return data;
        }
        return default;
    }
}
