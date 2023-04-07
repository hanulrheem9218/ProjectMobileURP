using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ConversationManager : MonoBehaviour
{
    [SerializeField] private ConversationTask[] tasks;

    void Awake()
    {
        tasks = GetComponentsInChildren<ConversationTask>();
        foreach (ConversationTask task in tasks)
        {
            task.transform.gameObject.SetActive(false);
        }
    }

    public void StartConversationTask(int i)
    {
        tasks[i].transform.gameObject.SetActive(true);
        tasks[i].StartConversation();
    }
}
