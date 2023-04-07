using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilityAI : MonoBehaviour
{
    //SINGLETON PATTERN MEANS ONLY SINGLE SCRIPT.
    public UtilityAI instance { private set; get; }
    private GameObject utilityAIPrefab;

    public Transform targetObject { private set; get; }
    private Transform drawOriginalTransform;
    private float drawDetectionRange;
    // public static UtilityAI getInstance()
    // {
    //     if (instance == null && utilityAIPrefab == null)
    //     {
    //         utilityAIPrefab = new GameObject();
    //         utilityAIPrefab.name = "UtilityAITools";
    //         utilityAIPrefab.AddComponent<UtilityAI>();
    //         instance = utilityAIPrefab.GetComponent<UtilityAI>();
    //         print("Utility Ai tool Instantiated");
    //     }
    //     return instance;
    // }
    // public UtilityAI()
    // {
    //     getInstance();
    // }
    ///<summary>Must use InvokeRepeating to avoid lags. </summary>
    public void CheckInRange(Transform originalTransform, float detectionRange, LayerMask targetMask)
    {
        float attackDistance = Mathf.Infinity;
        Collider[] targets = Physics.OverlapSphere(originalTransform.position, detectionRange, targetMask);
        GameObject nearestTarget = null;
        foreach (Collider target in targets)
        {
            float distanceToTarget = Vector3.Distance(originalTransform.position, target.transform.position);
            if (distanceToTarget < attackDistance)
            {
                attackDistance = distanceToTarget;
                nearestTarget = target.gameObject;
            }
        }

        if (nearestTarget != null && attackDistance <= detectionRange)
        {
            targetObject = nearestTarget.transform;
        }
        else
        {
            targetObject = null;

        }
        // Debug.Log(targetObject);
    }
}
