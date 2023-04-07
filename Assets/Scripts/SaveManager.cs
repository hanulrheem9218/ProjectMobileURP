using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    delegate void saveFunction();
    delegate void loadFunction();

    private saveFunction saveME;
    private loadFunction LoadMe;
    // Start is called before the first frame update
    void Start()
    {

        saveME = EnemySave;
        saveME += PlayerSave;

        LoadMe = EnemyLoad;
        LoadMe += PlayerLoad;
    }

    // Update is called once per frame

    public EnemyScript[] GetEnemyTypes()
    {
        return FindObjectsOfType<EnemyScript>();
    }
    public void EnemySave()
    {
        foreach (EnemyScript enemy in FindObjectsOfType<EnemyScript>())
        {
            SavingSystem.SaveData<EnemyScript>(enemy);
            print(enemy.GetInstanceID());

        }
    }

    public void EnemyLoad()
    {
        object typeObject = SavingSystem.LoadData<TransferData<EnemyScript>, EnemyScript>();
        //  Debug.Log(typeObject);
        TransferData<EnemyScript> enemyScript = (TransferData<EnemyScript>)typeObject;
        for (int i = 0; i < GetEnemyTypes().Length; i++)
        {
            //Enemy Positions.
            Vector3 enemyPosition = new Vector3(enemyScript.enemyPositions[0, i], enemyScript.enemyPositions[1, i], enemyScript.enemyPositions[2, i]);
            GetEnemyTypes()[i].enemyTransform.position = enemyPosition;
            //Enemy Rotations.
            Vector3 enemyRotation = new Vector3(enemyScript.enemyRotations[0, i], enemyScript.enemyRotations[1, i], enemyScript.enemyRotations[2, i]);
            GetEnemyTypes()[i].enemyTransform.localEulerAngles = enemyRotation;
        }

    }
    public void AllSave()
    {
        saveME();
    }

    public void AllLoad()
    {
        LoadMe();
    }
    void Update()
    {

    }
    public void PlayerSave()
    {
        //Saving system file with generic type.
        SavingSystem.SaveData<PlayerGeneralSystem>(FindObjectOfType<PlayerGeneralSystem>());
    }

    public void PlayerLoad()
    {
        //getting the values fundamental of values.
        object typeObject = SavingSystem.LoadData<TransferData<PlayerGeneralSystem>, PlayerGeneralSystem>();
        Debug.Log(typeObject);
        TransferData<PlayerGeneralSystem> player = (TransferData<PlayerGeneralSystem>)typeObject;
        Debug.Log(player.positions[0] + ": " + player.positions[1] + ": " + player.positions[2]);
        Vector3 playerPosition = new Vector3(player.positions[0], player.positions[1], player.positions[2]);
        FindObjectOfType<PlayerGeneralSystem>().playerObject.transform.position = playerPosition;
    }
}
