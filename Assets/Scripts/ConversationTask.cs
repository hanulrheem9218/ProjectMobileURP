using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class ConversationTask : MonoBehaviour
{
    [SerializeField] private Conversation[] conversations;
    [SerializeField] private Dialogue dialogue;
    [SerializeField] private Coroutine waitForConversation;
    [SerializeField] private Coroutine waitForSentence;
    void Awake()
    {
        conversations = GetComponentsInChildren<Conversation>();
        for (int i = 0; i < conversations.Length; i++)
        {
            conversations[i].GetComponentInChildren<TextMeshProUGUI>().text = dialogue.name + " : " + dialogue.sentences[i];
        }
    }

    public void StartConversation()
    {
        waitForConversation = StartCoroutine(NextConversation());
    }

    IEnumerator TypeSentence(TextMeshProUGUI conversation, string sentence, float delaySecond)
    {

        conversation.text = dialogue.name + " : ";

        foreach (char letter in sentence.ToCharArray())
        {
            conversation.text += letter;
            yield return new WaitForSeconds(delaySecond);
        }
        print("Allowed to touch");
    }

    IEnumerator NextConversation()
    {
        for (int i = 0; i < conversations.Length; i++)
        {
            conversations[i].GetComponent<Animator>().SetTrigger("SlideUp");
            waitForSentence = StartCoroutine(TypeSentence(conversations[i].GetComponentInChildren<TextMeshProUGUI>(), dialogue.sentences[i], 0.04f));
            yield return new WaitForSeconds((dialogue.sentences[i].Length / 2f) / 5f);
            conversations[i].GetComponent<Animator>().SetTrigger("SlideDown");
            StopCoroutine(waitForSentence);
            //conversations[i].gameObject.SetActive(false);
        }
        StopCoroutine(waitForConversation);
    }
}
