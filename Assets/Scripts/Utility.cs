using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using Cinemachine;
public class Utility : MonoBehaviour
{
    // Start is called before the first frame update
    private static Utility instance;
    private static GameObject utilityPrefab;
    private static AsyncOperation operation;
    private static Coroutine waitForScene;

    private static CinemachineVirtualCamera cinemachineVirtualCamera;
    private static bool isIntensity;
    private static float shakeIntensity;
    private static float shakeEndTime;
    private static float shakeTime;
    private static Coroutine waitForCameraShake;
    //Singleton case to brint them method.
    public static void SetFrame30FPS()
    {
        Application.targetFrameRate = 30;
    }
    public static void SetFrame60FPS()
    {
        Application.targetFrameRate = 60;
    }
    public static Utility getInstance()
    {
        if (instance == null && utilityPrefab == null)
        {
            utilityPrefab = new GameObject();
            utilityPrefab.name = "UtilityTools";
            utilityPrefab.AddComponent<Utility>();
            instance = utilityPrefab.GetComponent<Utility>();
            print("Utility Instantiated");
        }
        return instance;
    }
    public static Slider setPlayerSliderUI(string sliderName, int maxAmount)
    {
        Slider slider = Utility.FindUIObjectWithName(sliderName).transform.GetComponent<Slider>();
        slider.minValue = 0f;
        slider.maxValue = maxAmount;
        slider.value = maxAmount;
        slider.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = slider.value.ToString();

        return slider;
    }
    public static void StartShakeCamera(MonoBehaviour coroutine, float intensity, float endTime)
    {
        if (waitForCameraShake != null)
        {
            coroutine.StopCoroutine(waitForCameraShake);
        }

        isIntensity = false;
        cinemachineVirtualCamera = GameObject.Find("PlayerCamera").GetComponent<CinemachineVirtualCamera>();
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
        shakeIntensity = intensity;
        shakeEndTime = endTime;
        shakeTime = endTime;
        waitForCameraShake = coroutine.StartCoroutine(ShakeCamera(coroutine));
    }

    static IEnumerator ShakeCamera(MonoBehaviour coroutine)
    {
        while (!isIntensity)
        {
            if (shakeTime > 0)
            {
                shakeTime -= Time.deltaTime;
                CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
                cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = Mathf.Lerp(shakeIntensity, 0f, (1f - shakeTime / shakeEndTime));
                yield return null;
            }
            else
            {
                isIntensity = true;
                coroutine.StopCoroutine(waitForCameraShake);
            }
        }
    }
    public Utility()
    {
        getInstance();
    }
    public static void LoadMe(int index)
    {
        SceneManager.LoadScene(index);
    }
    public static void setStatusUI(float maxAmount, string objectName, Transform transformUI)
    {
        Transform copyTransformUI = transformUI.Find("StatusUI").transform;
        Slider slider = copyTransformUI.GetComponentInChildren<Slider>();
        TextMeshProUGUI objectText = copyTransformUI.GetComponentInChildren<TextMeshProUGUI>();
        slider.maxValue = maxAmount;
        slider.value = maxAmount;
        slider.minValue = 0;
        objectText.text = objectName;
    }

    public static void StatusChangeAmount(MonoBehaviour mono, float amount, Transform trasnformUI)
    {
        // called on once not good for 
        Slider sliderUI = trasnformUI.GetComponentInChildren<Slider>();
        mono.StartCoroutine(decreaseStatus(sliderUI, (int)amount, 1));
    }
    private static IEnumerator decreaseStatus(Slider sliderUI, int amount, float delay)
    {
        for (int i = 1; i <= amount; i++)
        {
            sliderUI.value--;
            yield return new WaitForSeconds(delay);
        }

    }
    public static void Invoke(MonoBehaviour mb, Action f, float delay)
    {
        mb.StartCoroutine(InvokeRoutine(f, delay));
    }

    private static IEnumerator InvokeRoutine(Action f, float delay)
    {
        yield return new WaitForSeconds(delay);
        f();
    }

    public static void CreateButtonSetup(GameObject original, string panelName, string backButtonName)
    {

        GameObject panelObject = FindGameObjectWithName(GameObject.Find("Canvas"), panelName);
        //print(panelObject);
        Button backButton = FindGameObjectWithName(GameObject.Find("Canvas"), backButtonName).GetComponent<Button>();
        original.GetComponent<Button>().onClick.AddListener(() => panelObject.SetActive(true));
        backButton.onClick.AddListener(() => panelObject.SetActive(false));
        panelObject.SetActive(false);

    }

    ///<summary> Finding transform with specific target name. </summary>
    public static GameObject FindGameObjectWithName(GameObject parent, string objectName)
    {
        Transform[] tempParent = parent.transform.GetComponentsInChildren<Transform>(true);
        foreach (Transform child in tempParent)
        {
            if (child.transform.name == objectName)
            {
                return child.gameObject;
            }
        }
        return null;
    }

    public static GameObject FindUIObjectWithName(string objectName)
    {
        GameObject parent = GameObject.Find("Canvas");
        Transform[] tempParent = parent.transform.GetComponentsInChildren<Transform>(true);
        foreach (Transform child in tempParent)
        {
            if (child.transform.name == objectName)
            {
                return child.gameObject;
            }
        }
        return null;
    }
    ///<summary> Setting up current user screen</summary>
    public static void ShowStatusUI(Transform originalTransform)
    {
        Transform transformUI = originalTransform.Find("StatusUI").transform;
        transformUI.LookAt(transformUI.transform.position + GameObject.FindGameObjectWithTag("MainCamera").transform.rotation * Vector3.forward, GameObject.FindGameObjectWithTag("MainCamera").transform.rotation * Vector3.up);
    }
    ///<summary> Automatically matching with the current user screen.  Low Vector2 number gets the bigger size, higher number gets lower size.</summary>
    public static RectTransform SetScreenSizeUI(GameObject parent, string objectName, Vector2 setSizeDelta, Vector2 setSquare, Vector3 setAnchoredPosition)
    {
        RectTransform uiSize;
        uiSize = FindGameObjectWithName(parent, objectName).GetComponent<RectTransform>();
        //Square paramter.
        if (setSquare != Vector2.zero)
        {
            uiSize.sizeDelta = new Vector2((Screen.width / setSquare.x), (Screen.width / setSquare.y));
        }
        if (setSizeDelta != Vector2.zero)
        {
            uiSize.sizeDelta = new Vector2((Screen.width / setSizeDelta.x), (Screen.height / setSizeDelta.y));
        }
        if (setAnchoredPosition != Vector3.zero)
        {
            uiSize.anchoredPosition = setAnchoredPosition;
        }
        return uiSize;
    }
    ///<summary> Automatically matching with the current user screen.  Low Vector2 number gets the bigger size, higher number gets lower size.</summary>
    public static RectTransform SetScreenSizeUI(string objectName, Vector2 setSizeDelta, Vector2 setSquare, Vector3 setAnchoredPosition)
    {
        return SetScreenSizeUI(GameObject.Find("Canvas"), objectName, setSizeDelta, setSquare, setAnchoredPosition);
    }
    public static RectTransform SetScreenSizeUI(string objectName, Vector2 setSizeDelta, Vector3 setAnchoredPosition)
    {
        return SetScreenSizeUI(GameObject.Find("Canvas"), objectName, setSizeDelta, Vector2.zero, setAnchoredPosition);
    }

    ///<summary> slider function for easy access. </summary>
    public static Slider setSliderUI(GameObject parent, string objectName, float min, float max, float value, bool isAllowed)
    {
        Slider slider;
        slider = FindGameObjectWithName(parent, objectName).GetComponent<Slider>();
        if (isAllowed)
        {
            slider.minValue = min;
            slider.maxValue = max;
            slider.value = value;
        }
        return slider;
    }
    ///<summary> when the endtime reached the code will be executued after the code. </summary>
    public static void manualCanvasFadeUI(CanvasGroup canvas, int nextScene, float endTime, float fadeSpeed, float delaySecond, bool isFadeOut, bool isFadeIn, MonoBehaviour instance)
    {
        if (isFadeIn)
        {
            canvas.alpha = 0f;
        }
        instance.StartCoroutine(manualCanvasFade(canvas, nextScene, endTime, fadeSpeed, delaySecond, isFadeOut, isFadeIn, instance));
    }

    public static void allowScene()
    {
        operation.allowSceneActivation = true;
    }
    private static IEnumerator manualCanvasFade(CanvasGroup canvas, int nextScene, float endTime, float fadeSpeed, float delaySecond, bool isFadeOut, bool isFadeIn, MonoBehaviour instance)
    {
        yield return new WaitForSeconds(endTime);
        while (isFadeOut)
        {
            float fadeAmount = canvas.alpha - (fadeSpeed * Time.deltaTime);
            canvas.alpha = fadeAmount;
            //  print(fadeAmount);
            yield return new WaitForSeconds(delaySecond);
            if (canvas.alpha <= 0)
            {
                isFadeOut = false;
                canvas.gameObject.SetActive(false);
                // move to next Scene.
                instance.StartCoroutine(loadAsync(nextScene));

            }
        }
        while (isFadeIn)
        {
            float fadeAmount = (fadeSpeed * Time.deltaTime);
            canvas.alpha += fadeAmount;
            yield return new WaitForSeconds(delaySecond);
            if (canvas.alpha >= 1)
            {
                isFadeIn = false;
                instance.StartCoroutine(loadAsync(nextScene));
            }
        }

    }
    static IEnumerator loadAsync(int index)
    {
        operation = SceneManager.LoadSceneAsync(index);
        operation.allowSceneActivation = false;
        while (!operation.isDone)
        {

            float progress = Mathf.Clamp01(operation.progress / .9f);
            // print(operation.ToString());
            FindGameObjectWithName(GameObject.Find("Canvas"), "CheckLoading").GetComponent<Slider>().value = progress;
            FindGameObjectWithName(GameObject.Find("Canvas"), "LoadText").GetComponent<TextMeshProUGUI>().text = progress * 100f + "%";
            //  print("LogProgress:" + progress);
            yield return null;
        }
    }

    public enum PRESET
    {
        MIDDLE_CENTER,
        BOTTOM_LEFT,
        BOTTOM_RIGHT,
        TOP_LEFT,
        TOP_RIGHT

    }
    public static void createPopUpMessage(bool isPopUp, GameObject parent, string objectName, PRESET POS, string title, string paragraph, Vector2 diff, Vector2 changeSize, Vector3 positionDiff)
    {
        //utility.FindGameObjectWithName(canvas, "VersionCheck").GetComponent<Button>()
        GameObject uiObject = FindGameObjectWithName(parent, objectName);
        // current button Button

        // get resource file and isntatinate.
        RectTransform rectTransform;
        GameObject uiObjectPrefab = Resources.Load("messagesUI/PopUpMessage") as GameObject;
        rectTransform = uiObjectPrefab.GetComponent<RectTransform>();
        print(uiObjectPrefab);
        RectTransform spawned = Instantiate(rectTransform, uiObject.transform);
        spawned.transform.name = "message";


        spawned.transform.Find("Title").GetComponent<TextMeshProUGUI>().text = title;
        spawned.transform.Find("Paragraph").GetComponent<TextMeshProUGUI>().text = paragraph;
        // ScreenAuteSizeUI(parent, objectName, diff, changeSize, positionDiff);
        //uiObject.GetComponent<TextMeshProUGUI>().text = textMessage;
        SetScreenSizeUI(parent, spawned.transform.name, diff, changeSize, positionDiff);
        FindGameObjectWithName(parent, "Close").GetComponent<Button>().onClick.AddListener(() => spawned.gameObject.SetActive(false));
        if (!isPopUp)
        {
            uiObject.GetComponent<Button>().onClick.AddListener(() => spawned.gameObject.SetActive(true));

            spawned.gameObject.SetActive(false);
        }
        else
        {
            spawned.gameObject.SetActive(true);
        }


        switch (POS)
        {
            case PRESET.MIDDLE_CENTER:
                {
                    spawned.anchorMax = new Vector2(0.5f, 0.5f);
                    spawned.anchorMin = new Vector2(0.5f, 0.5f);
                    spawned.pivot = new Vector2(0.5f, 0.5f);
                    break;
                }
            case PRESET.BOTTOM_LEFT:
                {
                    spawned.anchorMax = new Vector2(0, 0);
                    spawned.anchorMin = new Vector2(0, 0);
                    spawned.pivot = new Vector2(0, 0);
                    break;
                }
            case PRESET.BOTTOM_RIGHT:
                {
                    spawned.anchorMax = new Vector2(1, 0);
                    spawned.anchorMin = new Vector2(1, 0);
                    spawned.pivot = new Vector2(1, 0);
                    break;
                }
            case PRESET.TOP_RIGHT:
                {
                    spawned.anchorMax = new Vector2(1, 1);
                    spawned.anchorMin = new Vector2(1, 1);
                    spawned.pivot = new Vector2(1, 1);
                    break;
                }
            case PRESET.TOP_LEFT:
                {
                    spawned.anchorMax = new Vector2(0, 1);
                    spawned.anchorMin = new Vector2(0, 1);
                    spawned.pivot = new Vector2(0, 1);
                    break;
                }
            default:
                {
                    Debug.LogError("cant find the position");
                    break;
                }
        }

    }

    public void animationWithUI()
    {

    }
}
