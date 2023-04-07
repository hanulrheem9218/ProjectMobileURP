using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class AbilityController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDragHandler, IEndDragHandler, IBeginDragHandler, IPointerClickHandler
{
    // Start is called before the first frame update
    [SerializeField] List<RectTransform> mainGlances;
    [SerializeField] private Transform glanceObjectUI;

    void Start()
    {

        checkCurrentGlances();

    }

    private void 하이(){
        int 떙떙이 = 0;
        int 살아있음 = 1;
        print(떙떙이 + 살아있음);
    }

    private void checkCurrentGlances()
    {

        Image[] glanceSkills = Utility.FindUIObjectWithName("GlanceSkills").GetComponentsInChildren<Image>();
        List<RectTransform> availableGlances = new List<RectTransform>();
        // Sorting Glances.
        for (int i = 0; i < glanceSkills.Length; i++)
        {
            if (glanceSkills[i].transform.gameObject.layer == (int)InventoryManager.UI.GLANCESLOT)
            {
                availableGlances.Add(glanceSkills[i].transform.GetComponent<RectTransform>());
                print(glanceSkills[i].transform.name);
            }
        }
        for (int j = 0; j < availableGlances.Count; j++)
        {
            if (availableGlances[j].transform.childCount <= 0)
            {
                mainGlances.Add(null);
            }
            else
            {
                mainGlances.Add(availableGlances[j].transform.GetChild(0).GetComponent<RectTransform>());
            }
        }
    }
    // Update is called once per frame


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
        if (touchInfo.GetComponent<GlanceUI>() != null && touchInfo.gameObject.layer == (int)InventoryManager.UI.GLANCE)
        {
            glanceObjectUI = touchInfo.transform;
            //   originalParent = uiObject.parent;
        }
    }
    public void OnDrag(PointerEventData e)
    {
        if (glanceObjectUI != null && e.dragging)
        {
            //testing purpose;
            if (glanceObjectUI.gameObject.layer == (int)InventoryManager.UI.GLANCE)
            {
                glanceObjectUI.transform.position = e.position;
                glanceObjectUI.GetComponent<Image>().raycastTarget = false;
            }
            else
            {
                print("Failed");
                glanceObjectUI.GetComponent<Image>().raycastTarget = true;
            }

        }

    }
    public void OnEndDrag(PointerEventData e)
    {
        if (glanceObjectUI != null)
        {
            //uiObject.GetComponent<Image>().raycastTarget = true;
            //this is current rayvcast.
            GameObject touchInfo = e.pointerCurrentRaycast.gameObject;
            GameObject enterObject = e.pointerEnter.gameObject;
            // rest uiObject
            //uiObject = touchInfo.transform;
            //  print("PARENT:" + enterObject.transform.parent.childCount);
            //   print(enterObject.transform.name);

            //print(enterObject.transform.parent);
            // print(glanceObjectUI.GetComponent<GlanceUI>().glancePos == enterObject.GetComponent<GlanceUI>().glancePos);
            if (glanceObjectUI.gameObject.layer == (int)InventoryManager.UI.GLANCE && (enterObject.layer == (int)InventoryManager.UI.GLANCE && enterObject.transform.parent.childCount == 1)
            && glanceObjectUI.GetComponent<GlanceUI>().glancePos == enterObject.GetComponent<GlanceUI>().glancePos && glanceObjectUI.transform.parent.transform.parent.name == "GlanceSkills")
            {
                print("switched" + glanceObjectUI.transform.childCount);
                //  InventoryManager.SwitchUI(glanceObjectUI.gameObject, enterObject);
                GlanceSwitchUI(glanceObjectUI, enterObject.transform);
            }
            // you have to add if condition.
            else if (glanceObjectUI.gameObject.layer == (int)InventoryManager.UI.GLANCE && (enterObject.layer == (int)InventoryManager.UI.GLANCESLOT && enterObject.transform.childCount == 0)
             && (glanceObjectUI.GetComponent<GlanceUI>().glancePos == enterObject.GetComponent<GlanceSlotUI>().glancePos || glanceObjectUI.GetComponent<GlanceUI>().glanceName == enterObject.GetComponent<GlanceSlotUI>().glanceName))
            {
                glanceObjectUI.SetParent(enterObject.transform);
                InventoryManager.ResetSelectedUI(glanceObjectUI);
                print("Placed");
            }
            //Inventory Section. only inventory Tag will get resetted.
            else if (glanceObjectUI.gameObject.layer == (int)InventoryManager.UI.GLANCE && enterObject.layer == (int)InventoryManager.UI.INVENTORY)
            {
                print("INventory detected");
                InventoryManager.ResetSelectedUI(glanceObjectUI);
            }
            //UI only
            else if (glanceObjectUI.gameObject.layer == (int)InventoryManager.UI.GLANCE && enterObject.layer == (int)InventoryManager.UI.UI)
            {
                print("Dont desotry");
                InventoryManager.ResetSelectedUI(glanceObjectUI);
                // InventoryManager.DropUI(glanceObjectUI);
            }
            InventoryManager.ResetSelectedUI(glanceObjectUI);

            // print("somethign else");
            //ResetSelectedUI();
            glanceObjectUI = null;
        }
    }

    private void GlanceSwitchUI(Transform original, Transform swapObject)
    {
        Transform[] parents = new Transform[2];
        parents[0] = original.transform.parent; //Item slot is the parent.
        GlanceSlotUI[] glanceLists = Utility.FindUIObjectWithName("GlanceLists").GetComponentsInChildren<GlanceSlotUI>();
        foreach (GlanceSlotUI slot in glanceLists)
        {
            if (original.GetComponent<GlanceUI>().glanceName == slot.glanceName)
            {

                print("ME " + slot.transform.name + "NAMES:" + original.GetComponent<GlanceUI>().glanceName + slot.glanceName + swapObject.transform.name);
                parents[1] = slot.transform;
                original.transform.SetParent(parents[1]);
                original.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
                original.GetComponent<Image>().raycastTarget = true;
                break;
            }
        }

        // swapObject.transform.SetParent(parents[0]);

        // swapObject.transform.SetParent(parents[0]);
        //only swap positions in item lists.
        switch (swapObject.GetComponent<GlanceUI>().glancePos)
        {
            case 1:
                {
                    Transform glanceOne = Utility.FindUIObjectWithName("Glance1").transform;
                    swapObject.SetParent(glanceOne);
                    break;
                }
            case 2:
                {
                    Transform glanceTwo = Utility.FindUIObjectWithName("Glance2").transform;
                    swapObject.SetParent(glanceTwo);
                    break;
                }
            case 3:
                {
                    Transform glanceThree = Utility.FindUIObjectWithName("Glance3").transform;
                    swapObject.SetParent(glanceThree);
                    break;
                }

        }
        swapObject.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
        swapObject.GetComponent<Image>().raycastTarget = true;

        // original.transform.SetParent(parents[1]);
        // original.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
        // original.GetComponent<Image>().raycastTarget = true;

    }
}
