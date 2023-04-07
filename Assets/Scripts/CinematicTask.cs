using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicTask : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private CinematicInterractible[] cinematicDialogues;
    [SerializeField] private Transform[] cutScenes;
    private Queue<string> sentences;
    private Coroutine waitForSentence;
    private int dialogueCounts { get; set; }
    private int sceneCounts;
    void Awake()
    {

        sentences = new Queue<string>();
        cinematicDialogues = GetComponentsInChildren<CinematicInterractible>();
        cutScenes = Utility.FindGameObjectWithName(transform.Find("CutScene").gameObject, "ImageScenes").GetComponentsInChildren<Transform>();
        dialogueCounts = 0;
        sceneCounts = 0;
        foreach (CinematicInterractible dialogue in cinematicDialogues)
        {
            dialogue.transform.gameObject.SetActive(false);
            //dialogue.nextDialogue.gameObject.SetActive(false);
        }
    }

    public void StartCinematic()
    {
        cinematicDialogues[dialogueCounts].transform.gameObject.SetActive(true);
        // cinematicDialogues[dialogueCounts].scenesRectTransform[sceneCounts].gameObject.SetActive(true);
        Debug.Log("Starting converasation with" + cinematicDialogues[dialogueCounts].dialogue.name);
        sentences.Clear();
        foreach (string sentence in cinematicDialogues[dialogueCounts].dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        NextSetenceWithScene();
    }
    public void NextSetenceWithScene()
    {
        print(sceneCounts);
        if (sentences.Count == 0)
        {
            sceneCounts = 0;
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        //  Debug.Log(sentence);
        cinematicDialogues[dialogueCounts].scenesRectTransform[sceneCounts].GetComponent<Animator>().SetTrigger("FadeIn");
        cinematicDialogues[dialogueCounts].nextDialogue.gameObject.SetActive(false);
        cinematicDialogues[dialogueCounts].sceneParagrah.text = sentence;
        if (waitForSentence != null)
        {
            StopCoroutine(waitForSentence);
        }
        sceneCounts++;

        waitForSentence = StartCoroutine(TypeSentence(sentence, 0.03f));
    }
    IEnumerator TypeSentence(string sentence, float delaySecond)
    {
        cinematicDialogues[dialogueCounts].sceneParagrah.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            cinematicDialogues[dialogueCounts].sceneParagrah.text += letter;
            yield return new WaitForSeconds(delaySecond);
        }
        print("Allowed to touch");
        cinematicDialogues[dialogueCounts].nextDialogue.gameObject.SetActive(true);
    }

    public void EndDialogue()
    {
        cinematicDialogues[dialogueCounts].transform.gameObject.SetActive(false);

        if (dialogueCounts == cinematicDialogues.Length - 1)
        {
            print(dialogueCounts + ": " + (cinematicDialogues.Length - 1));
            FindObjectOfType<InGamePlaySystemUI>().ShowGamePlay();
            Debug.Log("end of conversation");
        }
        else
        {
            print(dialogueCounts + ": " + (cinematicDialogues.Length - 1));
            dialogueCounts++;
            cinematicDialogues[dialogueCounts].transform.gameObject.SetActive(true);
            StartCinematic();
        }

    }
}
