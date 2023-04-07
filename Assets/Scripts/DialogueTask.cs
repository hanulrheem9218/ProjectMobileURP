using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class DialogueTask : MonoBehaviour
{
    // Start is called before the first frame update

    //First in First out system.
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    [SerializeField] private DialogueInterractible[] characterDialogues;
    private Queue<string> sentences;
    private Coroutine waitForSentence;
    [SerializeField] private int dialogueCounts;
    void Awake()
    {
        sentences = new Queue<string>();
        characterDialogues = GetComponentsInChildren<DialogueInterractible>();
        dialogueCounts = 0;
        foreach (DialogueInterractible dialogue in characterDialogues)
        {
            dialogue.transform.gameObject.SetActive(false);
        }
    }


    public void StartDialogue()
    {

        characterDialogues[dialogueCounts].transform.gameObject.SetActive(true);
        Debug.Log("Starting converasation with" + characterDialogues[dialogueCounts].dialogue.name);
        sentences.Clear();
        foreach (string sentence in characterDialogues[dialogueCounts].dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        //  Debug.Log(sentence);
        characterDialogues[dialogueCounts].nextDialogue.gameObject.SetActive(false);
        characterDialogues[dialogueCounts].characterParagraph.text = sentence;
        if (waitForSentence != null)
        {
            StopCoroutine(waitForSentence);
        }

        waitForSentence = StartCoroutine(TypeSentence(sentence, 0.03f));

    }
    IEnumerator TypeSentence(string sentence, float delaySecond)
    {
        characterDialogues[dialogueCounts].characterParagraph.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            characterDialogues[dialogueCounts].characterParagraph.text += letter;
            yield return new WaitForSeconds(delaySecond);
        }
        print("Allowed to touch");
        characterDialogues[dialogueCounts].nextDialogue.gameObject.SetActive(true);
    }
    public void EndDialogue()
    {


        characterDialogues[dialogueCounts].transform.gameObject.SetActive(false);

        if (dialogueCounts == characterDialogues.Length - 1)
        {
            print(dialogueCounts + ": " + (characterDialogues.Length - 1));
            FindObjectOfType<InGamePlaySystemUI>().ShowGamePlay();
            Debug.Log("end of conversation");
        }
        else
        {
            print(dialogueCounts + ": " + (characterDialogues.Length - 1));
            dialogueCounts++;
            characterDialogues[dialogueCounts].transform.gameObject.SetActive(true);
            StartDialogue();
        }


    }

}
