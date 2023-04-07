using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMiniMap : MonoBehaviour
{
    // [SerializeField] Transform playerTransform;
    [SerializeField] Transform currentMiniMapObject;
    [SerializeField] int viewDifference;
    void Start()
    {
        //Utility utility = Utility.getInstance();
        currentMiniMapObject = GameObject.Find("playerMap").transform;

        //playerTransform = GameObject.FindWithTag("Player").transform;
        //  playerTransform.transform.position += new Vector3(0f, 3, 0f);
        // mainCamera = GameObject.FindWithTag("MainCamera").transform;
    }

    // Update is called once per frame
    void Update()
    {
        currentMiniMapObject.transform.position = transform.position + new Vector3(0f, viewDifference, 0f);
        //  currentMiniMapObject.LookAt(playerTransform, Vector3.up + mainCamera.localPosition);
    }
}
