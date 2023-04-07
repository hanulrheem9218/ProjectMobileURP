using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
public class ActionControls : MonoBehaviour
{
    // Start is called before the first frame update
    private float currentScreenX;
    private float currentScreenY;
    private bool isScreenChanged;

    [SerializeField] private RectTransform interaction;
    private UnityAction interactionAction;
    private UnityEvent interactionEvent;
    //this script needs to be optimized entirely.

    // this goes in here.

    // private ActionTouch[] touches;
    void Awake()
    {
        //defaultAttack = transform.GetChild(0).GetComponent<Button>();
        GameObject canvas = GameObject.Find("Canvas");
        interaction = Utility.FindUIObjectWithName("Interaction").GetComponent<RectTransform>();
        currentScreenX = (Screen.width / 2);
        currentScreenY = (Screen.height / 2);
        Utility.SetScreenSizeUI(canvas, "Action", new Vector2(2, 2), Vector2.zero, Vector3.zero);
        Utility.SetScreenSizeUI(canvas, "Default", Vector2.zero, new Vector2(16, 16), new Vector3((currentScreenX / 14), -(currentScreenY / 16), 0));
        Utility.SetScreenSizeUI(canvas, "ActionOne", Vector2.zero, new Vector2(11, 11), new Vector3(-(currentScreenX / 10), -(currentScreenY / 12), 0));
        Utility.SetScreenSizeUI(canvas, "ActionTwo", Vector2.zero, new Vector2(11, 11), new Vector3(-(currentScreenX / 24), (currentScreenY / 5), 0));
        Utility.SetScreenSizeUI(canvas, "ActionThr", Vector2.zero, new Vector2(11, 11), new Vector3((currentScreenX / 10), (currentScreenY / 4), 0));
        Utility.SetScreenSizeUI(canvas, "Interaction", Vector2.zero, new Vector2(22, 22), new Vector3((currentScreenX / 4), (currentScreenY / 5), 0));
        Utility.SetScreenSizeUI(canvas, "Heal", Vector2.zero, new Vector2(22, 22), new Vector3(-(currentScreenX / 4), -(currentScreenY / 3), 0));

        Utility.SetScreenSizeUI("ActionCancel", Vector2.zero, new Vector2(22, 22), new Vector3((currentScreenX / 3), (currentScreenY / 3), 0));
    }

    public void SetInteraction(bool activeStatus)
    {
        interaction.gameObject.SetActive(activeStatus);
    }

    public void SetInteractionAction(UnityAction action)
    {
        interactionEvent = interaction.GetComponent<Button>().onClick;
        interactionAction = action;
        interactionEvent.AddListener(action);
    }

    public void RemoveInteractionListener()
    {
        if (interactionEvent != null)
        {
            interactionEvent.RemoveListener(interactionAction);
            interactionEvent = null;
            interactionAction = null;
        }
    }
}
