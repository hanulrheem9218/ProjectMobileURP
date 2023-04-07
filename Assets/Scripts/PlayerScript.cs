using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PlayerScript : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI[] texts;
    // Data that saves
    public Transform playerObject;

    // public Quaternion playerRotation;
    void Start()
    {
        playerObject = gameObject.transform;


    }

    // Update is called once per frame
    void Update()
    {
        texts[0].text = "" + playerObject.transform.position.x.ToString("0");
        texts[1].text = "" + playerObject.transform.position.y.ToString("0");
        texts[2].text = "" + playerObject.transform.position.z.ToString("0");

    }


}
