using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conversation : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        // RectTransform cineBar = Utility.FindGameObjectWithName(GameObject.Find("Canvas"), "BottomBar").GetComponent<RectTransform>();
        RectTransform conversation = transform.GetComponent<RectTransform>();
        conversation.sizeDelta = new Vector2((Screen.width / 3), 50f);
        conversation.anchoredPosition = new Vector3(0f, (Screen.height / 4f) * 1.6f, 0f);

    }

}
