using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class AbilityTouch : MonoBehaviour, IPointerClickHandler, IPointerExitHandler, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    // Start is called before the first frame update
    [SerializeField] private Transform actionCancel;
    [SerializeField] private Transform actionBg;
    [SerializeField] private Transform uiObject;
    [SerializeField] private bool isRadiusTouch;
    [SerializeField] private bool isStaticTouch;
    [SerializeField] private Image imgJoystickBg;
    [SerializeField] private Image imgJoyStick;
    private Vector2 posInput;
    private Vector2 touchAreaInput;
    private Vector2 originalPosition;
    void Start()
    {
        actionCancel = Utility.FindUIObjectWithName("ActionCancel").transform;
        actionBg = transform.Find("ActionBg").transform;
        if (actionCancel.gameObject.activeSelf)
        {
            actionCancel.gameObject.SetActive(false);
        }
        if (actionBg.GetComponent<Image>().enabled)
        {
            actionBg.GetComponent<Image>().enabled = false;
        }
        float currentScreenX = (Screen.width);
        float currentScreenY = (Screen.height);

        imgJoystickBg = transform.Find("ActionBg").GetComponent<Image>();
        imgJoystickBg.GetComponent<RectTransform>().sizeDelta = new Vector2(currentScreenY / 5, currentScreenY / 5);
        imgJoyStick = imgJoystickBg.transform.Find("ActionJoyStick").GetComponent<Image>();
        originalPosition = transform.GetComponent<RectTransform>().anchoredPosition;
    }
    private void toggleCancel()
    {
        actionCancel.gameObject.SetActive(!actionCancel.gameObject.activeSelf);
        actionBg.GetComponent<Image>().enabled = !actionBg.GetComponent<Image>().enabled;
    }
    void Update()
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
        if (uiObject == null && isRadiusTouch)
        {
            GameObject touchInfo = e.pointerCurrentRaycast.gameObject;
            uiObject = touchInfo.transform;
            toggleCancel();
        }

    }

    public void OnDrag(PointerEventData e)
    {
        if (uiObject != null && isRadiusTouch)
        {
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(imgJoystickBg.rectTransform, e.position, e.enterEventCamera, out posInput))
            {
                posInput.x = posInput.x / (imgJoystickBg.rectTransform.sizeDelta.x);
                posInput.y = posInput.y / (imgJoystickBg.rectTransform.sizeDelta.y);


                if (posInput.magnitude > 1.0f) // if its less becomes 0 or 1
                {
                    posInput = posInput.normalized;
                }
                //  playerObject.transform.position += transform.forward * 10 * Time.deltaTime;
                imgJoyStick.rectTransform.anchoredPosition = new Vector2(posInput.x * (imgJoystickBg.rectTransform.sizeDelta.x - 100 / 1), posInput.y * (imgJoystickBg.rectTransform.sizeDelta.y - 100 / 1));

            }

        }
    }
    public void OnPointerDown(PointerEventData e)
    {
        // if (RectTransformUtility.ScreenPointToLocalPointInRectangle(touchField, e.position, e.enterEventCamera, out touchAreaInput))
        // {
        //     // Debug.Log("Area Input: " + touchAreaInput.x.ToString() + "/" + touchAreaInput.y.ToString());
        //     touchContainer.anchoredPosition = new Vector3(touchAreaInput.x - 200, touchAreaInput.y - 200, 0);
        // }
        // OnDrag(eventData);
    }
    public void OnEndDrag(PointerEventData e)
    {
        if (uiObject != null && isRadiusTouch)
        {
            GameObject touchInfo = e.pointerCurrentRaycast.gameObject;
            GameObject enterObject = e.pointerEnter.gameObject;

            if (enterObject.transform.name == actionCancel.name)
            {
                print("CANCEL");
                imgJoyStick.rectTransform.anchoredPosition = Vector2.zero;
                toggleCancel();
                uiObject = null;
            }
            else
            {
                print("ABILITY!");
                imgJoyStick.rectTransform.anchoredPosition = Vector2.zero;
                toggleCancel();
                uiObject = null;
            }

        }
    }
    // Update is called once per frame

}
