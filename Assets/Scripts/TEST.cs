using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST : MonoBehaviour
{
    // Start is called before the first frame update
    void OnCollisionEnter(Collision collision)
    {
        print("enter");
        if (collision != null)
        {
            FindObjectOfType<InGamePlaySystemUI>().ShowCinematic();
        }

    }
    void OnCollisionExit(Collision collision)
    {
        FindObjectOfType<InGamePlaySystemUI>().ShowGamePlay();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
