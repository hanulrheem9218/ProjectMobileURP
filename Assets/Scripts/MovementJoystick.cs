using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class MovementJoystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    public RectTransform touchField;
    public bool isDragging;
    private float currentScreenX;
    private float currentScreenY;
    private Image imgJoystickBg;
    private Image imgJoyStick;
    private Vector2 posInput;
    private Vector2 touchAreaInput;
    // custom built.
    private RectTransform touchContainer;



    void Start()
    {

        imgJoystickBg = transform.GetChild(0).GetChild(0).GetComponent<Image>();
        touchContainer = transform.GetChild(0).GetComponent<RectTransform>();
        imgJoyStick = transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>();

        touchField = GetComponent<RectTransform>();
        currentScreenX = (Screen.width / 2f);
        currentScreenY = (Screen.height / 2f);


        if (touchField && imgJoystickBg)
        {
            //imgJoystickBg.rectTransform.anchoredPosition = new Vector3(-(currentScreenX / 8), -(currentScreenY / 10), 0);
            touchContainer.anchoredPosition = new Vector2((currentScreenX / 7), (currentScreenY / 6));
            imgJoystickBg.rectTransform.sizeDelta = new Vector2((currentScreenX / 4), (currentScreenX / 4));
            imgJoyStick.rectTransform.sizeDelta = new Vector2((currentScreenX / 8), (currentScreenX / 8));
            touchField.sizeDelta = new Vector2(currentScreenX, currentScreenY);


        }

    }
    public void OnDrag(PointerEventData eventData)
    {
        isDragging = true;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            imgJoystickBg.rectTransform, eventData.position, eventData.enterEventCamera, out posInput))
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

    public void OnPointerDown(PointerEventData eventData)
    {

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(touchField, eventData.position, eventData.enterEventCamera, out touchAreaInput))
        {
            // Debug.Log("Area Input: " + touchAreaInput.x.ToString() + "/" + touchAreaInput.y.ToString());
            touchContainer.anchoredPosition = new Vector3(touchAreaInput.x - 200, touchAreaInput.y - 200, 0);
        }
        OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isDragging = false;
        posInput = Vector2.zero;
        touchContainer.anchoredPosition = new Vector2((currentScreenX / 7), (currentScreenY / 6));
        imgJoyStick.rectTransform.anchoredPosition = Vector2.zero;
    }


    public float inputHorizontal()
    {
        if (posInput.x != 0)
        {
            return posInput.x;
        }
        return Input.GetAxis("Horizontal");
    }
    public float inputVertical()
    {
        if (posInput.y != 0)
        {
            return posInput.y;
        }
        return Input.GetAxis("Vertical");
    }


}
