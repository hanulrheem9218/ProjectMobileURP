using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ItemUI : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public bool isStackable;
    [SerializeField] private TextMeshProUGUI counts;
    [SerializeField] public int itemCount;
    [SerializeField] public int itemMax;
    [SerializeField] private string itemName;

    void Start()
    {
        counts = transform.GetComponentInChildren<TextMeshProUGUI>();
        // itemCount = 1;
        //counts.text = itemCount.ToString();
        if (!isStackable)
        {
            counts.text = "";
            //  counts.gameObject.SetActive(false);
            //    transform.GetComponentInChildren<GameObject>().SetActive(false);
        }
        else
        {
            counts.text = itemCount.ToString();
            //counts.gameObject.SetActive(true);
        }
    }
    public string GetName()
    {
        return itemName + "UI";
    }
    public void AddCount(int amount)
    {
        itemCount += amount;
    }
    public void SpawnItem()
    {   //when its spawning pass the itemcounts.
        UtilityResource.InstantiateResourcePlayer(itemName, itemCount);
    }
    // Update is called once per frame
    void Update()
    {
        counts.text = itemCount.ToString("##");
    }
}
