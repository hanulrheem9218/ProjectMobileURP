using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glance : MonoBehaviour
{
    // Start is called before the first frame update
    private bool allowPickup;
    [SerializeField] private string glanceName;
    void Start()
    {
        allowPickup = false;
    }

    // Update is called once per frame
    void Update()
    {
        Utility.ShowStatusUI(transform);
    }

    public string getName()
    {
        return glanceName;
    }
    void OnCollisionEnter(Collision collision)
    {
        GameObject itemObject = collision.transform.gameObject;

        if (itemObject.layer == 6 && !allowPickup) // Ground
        {
            transform.GetComponent<Rigidbody>().isKinematic = true;
            allowPickup = true;

        }
        else if (itemObject.layer == 9 && allowPickup) // Player
        {
            //this function should stack it automatically.
            UtilityResource.InstantiateGlanceInventory(glanceName + "UI", this);
            // Destroy(gameObject);
        }
    }
}
