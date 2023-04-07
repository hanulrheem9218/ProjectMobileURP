using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private DialogueTask[] dialogueTasks;
    void Start()
    {
        dialogueTasks = transform.GetComponentsInChildren<DialogueTask>();
        foreach (DialogueTask task in dialogueTasks)
        {
            task.transform.gameObject.SetActive(false);
        }
    }

    public void SetDialogueTask(int i)
    {
        dialogueTasks[i].gameObject.SetActive(true);
        dialogueTasks[i].StartDialogue();
    }

}
