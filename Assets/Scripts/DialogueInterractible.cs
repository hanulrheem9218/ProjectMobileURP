using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class DialogueInterractible : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private bool isLeft;
    public Dialogue dialogue;
    public TextMeshProUGUI characterName;
    public TextMeshProUGUI characterParagraph;
    public RectTransform nextDialogue;
    public Sprite characterImage;
    void Awake()
    {

        RectTransform charDialogue = transform.Find("CharacterDialogue").GetComponent<RectTransform>();
        RectTransform charName = transform.Find("CharacterName").GetComponent<RectTransform>();
        //  RectTransform cineBarSize = Utility.FindGameObjectWithName(GameObject.Find("Canvas"), "BottomBar").GetComponent<RectTransform>();
        RectTransform portrait = transform.Find("Portrait").GetComponent<RectTransform>();
        nextDialogue = Utility.FindGameObjectWithName(transform.gameObject, "InteractNext").GetComponent<RectTransform>();
        characterParagraph = transform.Find("CharacterDialogue").GetComponentInChildren<TextMeshProUGUI>();
        characterName = transform.Find("CharacterName").GetComponentInChildren<TextMeshProUGUI>();
        transform.Find("Portrait").GetComponent<Image>().sprite = characterImage;
        characterName.text = dialogue.name;
        portrait.sizeDelta = new Vector2((Screen.height / 1.5f), (Screen.height / 1.5f));

        charDialogue.sizeDelta = new Vector2((Screen.width / 1.5f), (Screen.height / 3f));
        charDialogue.anchoredPosition = new Vector3(0f, ((Screen.height / 4f) / 3.5f), 0f);
        //cineBarSize changes get an alterniatve way.
        Debug.Log("" + (Screen.height / 4f));
        charName.sizeDelta = new Vector2((Screen.width / 8f), (Screen.height / 10f));
        charName.anchoredPosition = new Vector3(-(charDialogue.sizeDelta.x / 3f), charDialogue.sizeDelta.y + ((Screen.height / 4f) / 3.5f), 0f);
        if (isLeft)
        {

            portrait.anchoredPosition = new Vector3(-(charDialogue.sizeDelta.x / 3f), (Screen.height / 7f), 0f);
        }
        else
        {
            portrait.anchoredPosition = new Vector3((charDialogue.sizeDelta.x / 3f), (Screen.height / 7f), 0f);
        }
        nextDialogue.sizeDelta = new Vector2((charDialogue.sizeDelta.y / 3), (charDialogue.sizeDelta.y / 3));

    }

}
