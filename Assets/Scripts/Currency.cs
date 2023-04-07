using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Currency : MonoBehaviour
{
    private static bool allowPickup;
    // Start is called before the first frame update
    void Start()
    {
        allowPickup = false;
    }

    // Update is called once per frame
    void Update()
    {
        Utility.ShowStatusUI(transform);
    }
    void OnCollisionEnter(Collision collision)
    {
        GameObject itemObject = collision.transform.gameObject;

        if (itemObject.layer == 6 && !allowPickup)
        {
            transform.GetComponent<Rigidbody>().isKinematic = true;
            allowPickup = true;
        }
        else if (itemObject.layer == 9 && allowPickup)
        {

            float randomCurrency = Random.Range(0.5f, 1f);
            PlayerStatus.AddCurency(randomCurrency);
            Destroy(gameObject);
        }
    }
}
