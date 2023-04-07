using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class InGameMenuLoading : MonoBehaviour
{
    [SerializeField] CanvasGroup makerLogo;
    [SerializeField] CanvasGroup fileCheckSystem;
    //  [SerializeField] CanvasGroup menu;
    //  [SerializeField] CanvasGroup chapters;

    [SerializeField] bool isFade;
    [SerializeField] bool isSceneLoading;
    [SerializeField] Button touchToStart;
    Utility utility;
    private RectTransform loadingBar;

    void Start()
    {
        GameObject canvas = GameObject.Find("Canvas");
        //    Utility.setFraneFPS(60);
        Utility.SetScreenSizeUI(canvas, "GameLogo", Vector2.zero, new Vector2(6, 6), new Vector3(0, -40, 0));
        Utility.SetScreenSizeUI(canvas, "CheckLoading", new Vector2(2, 80), Vector2.zero, Vector3.zero);
        Utility.SetScreenSizeUI(canvas, "VersionCheck", Vector2.zero, new Vector2(40, 40), new Vector3(30, 30, 0));
        makerLogo = Utility.FindGameObjectWithName(canvas, "MakerLogo").GetComponent<CanvasGroup>();
        Utility.SetScreenSizeUI(canvas, "CompanyLogo", Vector2.zero, new Vector2(3, 3), Vector3.zero);

        fileCheckSystem = Utility.FindGameObjectWithName(canvas, "FileCheckSystem").GetComponent<CanvasGroup>();
        touchToStart = Utility.FindGameObjectWithName(canvas, "TouchToStart").GetComponent<Button>();
        touchToStart.onClick.AddListener(Utility.allowScene);
        //Animation Stage.
        Utility.manualCanvasFadeUI(makerLogo, 1, 7, 1f, 0.001f, true, false, this);
        //other UIS

        // checking with the code.
        print(Utility.FindGameObjectWithName(canvas, "VersionCheck").GetComponent<Button>());
        Utility.createPopUpMessage(false, canvas, "VersionCheck", Utility.PRESET.BOTTOM_LEFT,
         "Build_Version 1.5.1", "Project Nemesis still in development process.",
          new Vector2(3, 3), Vector2.zero, new Vector3(20, 20, 0));

        Utility.createPopUpMessage(true, canvas, "FileCheckSystem", Utility.PRESET.MIDDLE_CENTER,
        "WARNING THIS IS ALPHA VERSION ", "This game dose not save any files, please be aware that this game is still in the development progress.",
         new Vector2(3, 3), Vector2.zero, Vector3.zero);
        // test evnironment 

    }



    // Update is called once per frame
    void Update()
    {

    }
    //Fading function by code

    // not a clean area.




}
