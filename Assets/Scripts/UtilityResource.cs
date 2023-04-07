using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UtilityResource : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private static GameObject resourceObject;
    private static Coroutine waitForObject;

    public static void InstantiateResourcePlayer(string objectName, int itemCount)
    {
        GameObject playerSpawnPosition = Utility.FindGameObjectWithName(GameObject.Find("Player"), "PlayerItemSpawn").gameObject;
        // you have to get your own prefab..
        if (resourceObject != null)
        {
            resourceObject = null;
        }

        resourceObject = Resources.Load("ItemAsset/" + objectName) as GameObject;
        print(resourceObject);
        GameObject temp = Instantiate(resourceObject, playerSpawnPosition.transform.position, Quaternion.identity);
        // if (itemCount == 1)
        // {
        temp.GetComponent<Item>().itemCounts = itemCount;
        //
        int itemX = Random.Range(-10, 10);
        int itemZ = Random.Range(-10, 10);
        temp.GetComponent<Rigidbody>().AddForce(new Vector3(itemX, 1, itemZ) * 25f, ForceMode.Acceleration);
        temp.name = objectName;

    }

    public static void InstantiateGlanceInventory(string objectName, Glance glance)
    {
        GlanceSlotUI[] glanceSlots = Utility.FindUIObjectWithName("GlanceLists").GetComponentsInChildren<GlanceSlotUI>();
        if (resourceObject != null)
        {
            resourceObject = null;
        }
        foreach (GlanceSlotUI slot in glanceSlots)
        {

            if (slot.transform.childCount <= 0 && slot.glanceName == glance.getName())
            {
                //   print(slot.transform.name);

                resourceObject = Resources.Load("UI/" + objectName) as GameObject;
                print(resourceObject + ": " + slot.name);
                GameObject temp = Instantiate(resourceObject, slot.GetComponent<RectTransform>().anchoredPosition, Quaternion.identity);
                temp.name = objectName;
                temp.transform.SetParent(slot.transform);
                temp.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
                Destroy(glance.gameObject);
                //print(slot.transform.GetChild(0).transform.name);
            }
        }
        //check if the slot child is empty and isnatiante object.
    }
    public static void InstatiateResourceInventory(string objectName, Item item)
    {
        RectTransform[] inventoryList = Utility.FindUIObjectWithName("InventoryItems").GetComponentsInChildren<RectTransform>(true);
        List<RectTransform> inveotrySlots = new List<RectTransform>();
        //  ItemUI itemUI = null;
        foreach (RectTransform slot in inventoryList)
        {
            if (slot.GetComponent<Item>() == null && slot.GetComponent<TextMeshProUGUI>() == null && slot.GetComponent<GridLayoutGroup>() == null)
            {
                inveotrySlots.Add(slot.GetComponent<RectTransform>());
            }
        }

        if (resourceObject != null)
        {
            resourceObject = null;
        }
        foreach (RectTransform itemSlot in inveotrySlots)
        {
            if (itemSlot.transform.childCount <= 0)
            {
                CreateObjectUI(itemSlot, objectName, item.itemCounts);
                print("Empty Child");
                Destroy(item.gameObject);
                break;
            }

            if (itemSlot.GetChild(0).GetComponent<ItemUI>() != null && itemSlot.GetChild(0).GetComponent<ItemUI>().itemCount != itemSlot.GetChild(0).GetComponent<ItemUI>().itemMax)
            {
                ItemUI itemUI = itemSlot.GetChild(0).GetComponent<ItemUI>();

                if (itemUI.GetName() == objectName && itemUI.itemCount < itemUI.itemMax)
                {
                    if (item.itemCounts == itemUI.itemMax)
                    {
                        print("yo");
                        CheckAndCreateObjectUI(inveotrySlots, objectName, item.itemCounts);
                        Destroy(item.gameObject);
                        break;
                    }
                    else
                    {
                        itemUI.AddCount(item.itemCounts);
                        Destroy(item.gameObject);
                        break;
                    }
                }


            }


        }
    }
    private static void CheckAndCreateObjectUI(List<RectTransform> itemInventory, string objectName, int itemCount)
    {


        // first look for all the slots (in inventory order) that have the item we want to place
        // we then iterate through these (in inventory order) and find the next slot that has not hit MAX
        foreach (RectTransform itemSlot in itemInventory)
        {
            // if the current object's name is the same as the one we want to place, and its not maxed, then we can place it here
            if (itemSlot.transform.childCount <= 0)
            {
                CreateObjectUI(itemSlot, objectName, itemCount);
                break;
            }


        }


    }

    private static void CreateObjectUI(RectTransform itemSlot, string objectName, int itemCount)
    {
        // add condition here.
        resourceObject = Resources.Load("UI/" + objectName) as GameObject;
        print(resourceObject + ": " + itemSlot.name);
        GameObject temp = Instantiate(resourceObject, itemSlot.GetComponent<RectTransform>().anchoredPosition, Quaternion.identity);
        temp.name = objectName;
        temp.GetComponent<ItemUI>().isStackable = true;
        temp.GetComponent<ItemUI>().itemCount = itemCount;
        temp.transform.SetParent(itemSlot);
        temp.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
    }
    public static void InstatiateResourceObject(string objectName, Transform target)
    {
        if (resourceObject != null)
        {
            resourceObject = null;
        }
        resourceObject = Resources.Load("ItemAsset/" + objectName) as GameObject;
        print(resourceObject);
        GameObject temp = Instantiate(resourceObject, target.transform.position, Quaternion.identity);
        int itemX = Random.Range(-10, 10);
        int itemZ = Random.Range(-10, 10);
        temp.GetComponent<Rigidbody>().AddForce(new Vector3(itemX, 10, itemZ) * 25f, ForceMode.Acceleration);
        temp.name = objectName;
    }


    public static void InstatiateResourceObject(MonoBehaviour mono, string objectName, int objectCount, Transform target)
    {
        waitForObject = mono.StartCoroutine(RepeatInstatiateObject(mono, objectName, objectCount, target));
    }
    private static IEnumerator RepeatInstatiateObject(MonoBehaviour mono, string objectName, int objectCount, Transform target)
    {
        if (resourceObject != null)
        {
            resourceObject = null;
        }
        for (int i = 0; i < objectCount; i++)
        {
            resourceObject = Resources.Load("ItemAsset/" + objectName) as GameObject;
            print(resourceObject);
            GameObject temp = Instantiate(resourceObject, target.transform.position, Quaternion.identity);
            int itemX = Random.Range(-10, 10);
            int itemZ = Random.Range(-10, 10);
            int itemY = Random.Range(7, 15);
            temp.GetComponent<Rigidbody>().AddForce(new Vector3(itemX, itemY, itemZ) * 25f, ForceMode.Acceleration);
            temp.name = objectName;
            yield return new WaitForSeconds(0.1f);
        }
        if (waitForObject != null)
        {
            mono.StopCoroutine(waitForObject);
            waitForObject = null;

        }
    }
}
