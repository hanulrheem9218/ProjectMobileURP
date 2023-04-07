using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    // Start is called before the first frame update
    private bool allowPickup;
    [SerializeField] private string partName;
    [SerializeField] public int itemCounts;
    [SerializeField] public int itemMax;
    void Start()
    {
        allowPickup = false;
        //        transform.GetComponent<BoxCollider>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        Utility.ShowStatusUI(transform);
    }

    public string GetName()
    {
        return partName;
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
            //this function should stack it automatically.
            UtilityResource.InstatiateResourceInventory(partName + "UI", this);
            // Destroy(gameObject);
        }
    }
}
