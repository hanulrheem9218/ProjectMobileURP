using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CinematicInterractible : MonoBehaviour
{
    public Dialogue dialogue;
    public TextMeshProUGUI sceneParagrah;
    public RectTransform nextDialogue;
    private CanvasGroup[] imageScenes;
    [SerializeField] public List<RectTransform> scenesRectTransform;
    void Awake()
    {
        RectTransform sceneDialogue = transform.Find("SceneDialogue").GetComponent<RectTransform>();
        RectTransform cineBarSize = Utility.FindGameObjectWithName(GameObject.Find("Canvas"), "BottomBar").GetComponent<RectTransform>();
        nextDialogue = Utility.FindGameObjectWithName(transform.gameObject, "InteractNext").GetComponent<RectTransform>();
        sceneParagrah = transform.Find("SceneDialogue").GetComponentInChildren<TextMeshProUGUI>();
        imageScenes = transform.Find("ImageScenes").GetComponentsInChildren<CanvasGroup>();
        sceneDialogue.sizeDelta = new Vector2((Screen.width / 1.5f), (Screen.height / 6f));
        sceneDialogue.anchoredPosition = new Vector3(0f, (cineBarSize.sizeDelta.y / 3.5f), 0f);
        nextDialogue.sizeDelta = new Vector2((sceneDialogue.sizeDelta.y / 2), (sceneDialogue.sizeDelta.y / 2));
        // u need to think for 
        scenesRectTransform = new List<RectTransform>();
        //int length = imageScenes.Length;
        foreach (CanvasGroup scene in imageScenes)
        {
            //scene.GetComponent<RectTransform>().sizeDelta = new Vector2((Screen.width / length), (Screen.height));
            scenesRectTransform.Add(scene.GetComponent<RectTransform>());
            //scene.gameObject.SetActive(false);
        }
    }


}
