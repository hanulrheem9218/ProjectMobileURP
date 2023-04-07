using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeNPC : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform targetObject;
    public float detectionRange;
    public LayerMask targetMask;
    private UtilityAI utilityAI;
    private ActionControls actionControls;
    private bool[] isConversation;

    [SerializeField] private RectTransform interaction;
    void Start()
    {
        utilityAI = new UtilityAI();
        actionControls = FindObjectOfType<ActionControls>();
        InvokeRepeating(nameof(CheckTransformTarget), 0, 0.4f);
        Utility.setStatusUI(100, "UPGRADE", transform);
        //interaction = Utility.FindUIObjectWithName("Interaction").GetComponent<RectTransform>();
        //        interaction.gameObject.SetActive(false);
        isConversation = new bool[2];
        //Setting up NPC UI

    }

    // Update is called once per frame
    void Update()
    {
        Utility.ShowStatusUI(transform);
        //  actionControls.SetInteraction(targetObject != null);

        if (targetObject != null && !isConversation[0])
        {
            actionControls.SetInteractionAction(() => { ShowUpgradePanel(); });
            isConversation[0] = true;
        }
        else if (targetObject == null && isConversation[0])
        {
            actionControls.RemoveInteractionListener();
            isConversation[0] = false;
        }
    }

    void ShowUpgradePanel()
    {
        FindObjectOfType<InGamePlaySystemUI>().ShowCinematic();
        FindObjectOfType<UpgradeManager>(true).ShowUpgrade();
    }

    void CheckTransformTarget()
    {
        this.targetObject = utilityAI.targetObject;
        utilityAI.CheckInRange(transform, detectionRange, targetMask);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
