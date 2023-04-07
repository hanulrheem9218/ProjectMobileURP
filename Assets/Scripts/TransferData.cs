using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TransferData<MonoData>
{
    //Transform object postion values;

    // this values should be shared together.
    public float[] positions;
    // saving multiple scripts now.
    // List<EnemyScript> enemyScript;
    public float[,] enemyPositions;
    public float[,] enemyRotations;

    public TransferData<MonoData> Instance(MonoData data)
    {
        if (data.GetType() == typeof(PlayerGeneralSystem))
        {
            positions = new float[3];
            var objectData = (PlayerGeneralSystem)(object)data;
            positions[0] = objectData.playerObject.transform.position.x;
            positions[1] = objectData.playerObject.transform.position.y;
            positions[2] = objectData.playerObject.transform.position.z;
        }
        else if (data.GetType() == typeof(EnemyScript))
        {

            var objectData = Object.FindObjectsOfType<EnemyScript>();
            enemyPositions = new float[3, objectData.Length];
            enemyRotations = new float[3, objectData.Length];
            for (int i = 0; i < objectData.Length; i++)
            {
                //Positions
                enemyPositions[0, i] = objectData[i].enemyTransform.transform.position.x;
                enemyPositions[1, i] = objectData[i].enemyTransform.transform.position.y;
                enemyPositions[2, i] = objectData[i].enemyTransform.transform.position.z;
                //Rotations
                enemyRotations[0, i] = objectData[i].enemyTransform.transform.rotation.eulerAngles.x;
                enemyRotations[1, i] = objectData[i].enemyTransform.transform.rotation.eulerAngles.y;
                enemyRotations[2, i] = objectData[i].enemyTransform.transform.rotation.eulerAngles.z;
            }

        }

        return this;
    }
}
