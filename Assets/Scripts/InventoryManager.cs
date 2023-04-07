using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
public class InventoryManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDragHandler, IEndDragHandler, IBeginDragHandler, IPointerClickHandler
{
    // Start is called before the first frame update
    public Transform uiObject;
    private Transform originalParent;
    public enum UI
    {
        UI = 5,
        ITEM = 11,
        ITEMSLOT = 12,
        INVENTORY = 13,
        GLANCESLOT = 14,
        GLANCE = 15
    };
    void Start()
    {

    }


    public void OnPointerClick(PointerEventData e)
    {
    }
    public void OnPointerEnter(PointerEventData e)
    {
    }

    public void OnPointerExit(PointerEventData e)
    {
    }
    public void OnBeginDrag(PointerEventData e)
    {
        GameObject touchInfo = e.pointerCurrentRaycast.gameObject;
        if (touchInfo.GetComponent<ItemUI>() != null && touchInfo.gameObject.layer == (int)UI.ITEM)
        {
            uiObject = touchInfo.transform;
            originalParent = uiObject.parent;
        }

    }
    public void OnDrag(PointerEventData e)
    {
        if (uiObject != null && e.dragging)
        {
            //testing purpose;
            if (uiObject.gameObject.layer == (int)UI.ITEM)
            {
                uiObject.transform.position = e.position;
                uiObject.GetComponent<Image>().raycastTarget = false;
            }
            else
            {
                print("Failed");
                uiObject.GetComponent<Image>().raycastTarget = true;
            }

        }



    }

    public void OnEndDrag(PointerEventData e)
    {
        if (uiObject != null)
        {
            //uiObject.GetComponent<Image>().raycastTarget = true;
            //this is current rayvcast.
            GameObject touchInfo = e.pointerCurrentRaycast.gameObject;
            GameObject enterObject = e.pointerEnter.gameObject;
            // rest uiObject
            //uiObject = touchInfo.transform;
            print("PARENT:" + enterObject.transform.parent.childCount);
            //   print(enterObject.transform.name);
            if (uiObject.gameObject.layer == (int)UI.ITEM && (enterObject.layer == (int)UI.ITEM && enterObject.transform.childCount == 1))
            {
                print("switched" + uiObject.transform.childCount);
                SwitchUI(uiObject.gameObject, enterObject);

            }
            // you have to add if condition.
            else if (uiObject.gameObject.layer == (int)UI.ITEM && (enterObject.layer == (int)UI.ITEMSLOT && enterObject.transform.childCount == 0))
            {
                uiObject.SetParent(enterObject.transform);
                ResetSelectedUI(uiObject);
                print("Placed");
            }
            //Inventory Section. only inventory Tag will get resetted.
            else if (uiObject.gameObject.layer == (int)UI.ITEM && enterObject.layer == (int)UI.INVENTORY)
            {
                print("INventory detected");
                ResetSelectedUI(uiObject);
            }
            //UI only
            else if (uiObject.gameObject.layer == (int)UI.ITEM && enterObject.layer == (int)UI.UI)
            {
                DropUI(uiObject);
            }
            ResetSelectedUI(uiObject);

            // print("somethign else");
            //ResetSelectedUI();
            uiObject = null;
        }
    }

    public static void ResetSelectedUI(Transform uiObject)
    {
        if (uiObject != null)
        {
            uiObject.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
            //  uiObject.parent = originalParent;
            uiObject.GetComponent<Image>().raycastTarget = true;
            uiObject = null;
        }
    }
    public static void DropUI(Transform uiObject)
    {
        if (uiObject != null)
        {
            uiObject.SendMessage("SpawnItem");
            Destroy(uiObject.gameObject);
        }
    }
    public static void SwitchUI(GameObject original, GameObject swapObject)
    {
        Transform[] parents = new Transform[2];
        parents[0] = original.transform.parent;
        parents[1] = swapObject.transform.parent;
        // GameObject temp = original;
        // uiObject.SetParent(swapObject.transform);
        // print("Original: " + original.name); // Item one
        //   print("Swap Object" + swapObject.name); //Item two.
        swapObject.transform.SetParent(parents[0]);
        swapObject.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
        swapObject.GetComponent<Image>().raycastTarget = true;

        original.transform.SetParent(parents[1]);
        original.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
        original.GetComponent<Image>().raycastTarget = true;

        //target 1 Original
        //target 2 copyOriginial
    }
}
