using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class NPC : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform targetObject;
    public float attackRange;
    public LayerMask targetMask;
    private bool[] isConversation;
    private GameObject canvas;
    private RectTransform interaction;
    private UnityEvent buttonEvent;
    private UnityAction buttonAction;
    private UtilityAI utilityAI;
    private ActionControls actionControls;
    // private Transform statusUI;
    void Start()
    {
        //must create new utilityAI to find the trasnform.
        utilityAI = new UtilityAI();
        actionControls = FindObjectOfType<ActionControls>();
        InvokeRepeating(nameof(CheckTransformTarget), 0, 0.4f);
        isConversation = new bool[2];
        canvas = GameObject.Find("Canvas");
        //UtilityAI.hi();

        Utility.setStatusUI(100, "NPC", transform);
        //        interaction = Utility.FindGameObjectWithName(canvas, "Interaction").GetComponent<RectTransform>();
        //interaction = Utility.FindUIObjectWithName("Interaction").GetComponent<RectTransform>();
        //    interaction.gameObject.SetActive(false);

    }

    // Update is called once per frame
    // you have to show interaction button when you are reached to area.
    void Update()
    {
        Utility.ShowStatusUI(transform);
        //actionControls.SetInteraction(targetObject != null);

        if (targetObject != null && !isConversation[0])
        {
            actionControls.SetInteractionAction(() => { ShowDialogueNPC(); });
            isConversation[0] = true;
        }
        else if (targetObject == null && isConversation[0])
        {
            actionControls.RemoveInteractionListener();
            isConversation[0] = false;
        }

        if (!isConversation[0])
        {
            transform.Find("StatusUI").Find("LocatorContainer").Find("Locator").gameObject.SetActive(true);
        }
        else
        {
            transform.Find("StatusUI").Find("LocatorContainer").Find("Locator").gameObject.SetActive(false);
        }
    }

    public void ShowDialogueNPC()
    {
        isConversation[0] = true;
        FindObjectOfType<InGamePlaySystemUI>().ShowCinematic();
        FindObjectOfType<DialogueManager>().SetDialogueTask(0);
        // buttonEvent.RemoveListener(buttonAction);
    }
    void CheckTransformTarget()
    {
        this.targetObject = utilityAI.targetObject;
        utilityAI.CheckInRange(transform, attackRange, targetMask);
    }
    // void OnCollisionEnter(Collision colli)
    // {
    //     if (colli.transform.name == "Player")
    //     {
    //         FindObjectOfType<ConversationManager>().StartConversationTask(0);
    //     }
    // }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
