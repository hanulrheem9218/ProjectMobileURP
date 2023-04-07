using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusText : MonoBehaviour
{
    // Start is called before the first frame update
    public float destoryTime = 3f;
    void Start()
    {
        Destroy(gameObject, destoryTime);
    }


}
