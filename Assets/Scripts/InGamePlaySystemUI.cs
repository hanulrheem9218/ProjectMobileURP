using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class InGamePlaySystemUI : MonoBehaviour
{
    // Start is called before the first frame update
    Utility utility;
    private RectTransform miniMap;
    private RectTransform playerMap;
    private RectTransform batteryInfo;
    private RectTransform batteryDisplay;
    private RectTransform quests;
    private RectTransform TopRight;
    private RectTransform playerProfile;
    private RectTransform dateTime;
    [SerializeField] private Slider batterySlider;
    [SerializeField] private TextMeshProUGUI textTime;
    private RectTransform playerXP;

    private Animator userPanel;
    private Animator cinematicPanel;
    void Awake()
    {
        Utility.SetFrame60FPS();
        GameObject canvas = GameObject.Find("Canvas");

        userPanel = Utility.FindGameObjectWithName(canvas, "UserPanel").GetComponent<Animator>();
        cinematicPanel = Utility.FindGameObjectWithName(canvas, "Cinematic").GetComponent<Animator>();

        //MiniMap
        miniMap = Utility.FindGameObjectWithName(canvas, "TopLeft").GetComponent<RectTransform>();
        playerMap = Utility.FindGameObjectWithName(canvas, "PlayerMap").GetComponent<RectTransform>();
        batteryInfo = Utility.FindGameObjectWithName(canvas, "BatteryStatus").GetComponent<RectTransform>();
        batterySlider = Utility.FindGameObjectWithName(canvas, "Slider").GetComponent<Slider>();
        //quests = utility.FindGameObjectWithName(canvas, "Quests").GetComponent<RectTransform>();
        Utility.setSliderUI(canvas, "Slider", 0, 1, SystemInfo.batteryLevel, true);
        //textTime = transform.GetChild(2).GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>();
        textTime = Utility.FindGameObjectWithName(canvas, "DateTime").GetComponent<TextMeshProUGUI>();
        //KilLogs
        TopRight = Utility.FindGameObjectWithName(canvas, "TopRight").GetComponent<RectTransform>();

        RectTransform TopLeft = Utility.FindGameObjectWithName(canvas, "TopLeft").GetComponent<RectTransform>();
        //PlayerPfrofile

        //Setting

        //        playerBg = Utility.FindGameObjectWithName(canvas, "PlayerBg").GetComponent<RectTransform>();
        // playerXP = Utility.FindGameObjectWithName(canvas, "PlayerXP").GetComponent<RectTransform>();
        //MiniMap 
        Utility.SetScreenSizeUI(canvas, "TopLeft", new Vector2(3, 2), Vector2.zero, Vector3.zero);
        Utility.SetScreenSizeUI(canvas, "PlayerMap", Vector2.zero, new Vector2(8, 8), new Vector3(-20, -20, 0));
        // utility.ScreenAuteSizeUI(canvas, "Quests", Vector2.zero, new Vector2(6, 8), new Vector3(-20, -20, 0));
        RectTransform batteryStatus = Utility.SetScreenSizeUI(canvas, "BatteryStatus", new Vector2(8, 30), Vector2.zero, new Vector3(-(playerMap.sizeDelta.x + 10f), -30f, 0));
        // Utility.SetScreenSizeUI("BatteryIcon", new Vector2(batteryStatus.sizeDelta.y, ), Vector2.zero);
        Utility.FindUIObjectWithName("BatteryIcon").GetComponent<RectTransform>().sizeDelta = new Vector2((batteryStatus.sizeDelta.y * 2), batteryStatus.sizeDelta.y);
        //Utility.SetScreenSizeUI(canvas, "Slider", new Vector2(batteryStatus.sizeDelta.y, batteryStatus.sizeDelta.y), Vector2.zero, Vector3.zero);
        Utility.SetScreenSizeUI(canvas, "DateTime", new Vector2(16, 30), Vector2.zero, Vector3.zero);
        //TopRight
        Utility.SetScreenSizeUI(canvas, "TopRight", new Vector2(3, 2), Vector2.zero, Vector3.zero);
        // utility.ScreenAuteSizeUI(canvas, "DeathLists", Vector2.zero, new Vector2(4, 10), new Vector3(20, -20, 0));
        Utility.SetScreenSizeUI(canvas, "GameMenu", new Vector2(8, 24), Vector2.zero, new Vector3(-20, -(playerMap.sizeDelta.y + 50), 0));
        RectTransform systemInven = Utility.SetScreenSizeUI(canvas, "SystemInven", new Vector2(16, 15), Vector2.zero, Vector3.zero);
        Utility.SetScreenSizeUI(canvas, "SystemMenu", new Vector2(16, 15), Vector2.zero, new Vector3(-(systemInven.sizeDelta.x), 0f, 0f));


        //PlayerProfile
        Utility.SetScreenSizeUI(canvas, "TopCenter", new Vector2(3, 2), Vector2.zero, Vector3.zero);

        // focuse on this
        RectTransform ProfileInfo = Utility.SetScreenSizeUI(canvas, "ProfileInfo", new Vector2(5, 15f), Vector2.zero, new Vector3(80, -80, 0));

        RectTransform healthBackground = Utility.SetScreenSizeUI("PlayerHealthBackground", new Vector2(5, 30f), new Vector2());
        Utility.SetScreenSizeUI("PlayerStaminaBackground", new Vector2(5, 30f), new Vector2(0f, -healthBackground.sizeDelta.y));
        //        Utility.ScreenAuteSizeUI(canvas, "PlayerBg", Vector2.zero, new Vector2(8 * 2, 8 * 2), Vector3.zero);
        //   Utility.ScreenAuteSizeUI(canvas, "PlayerItems", new Vector2(16, 26), Vector2.zero, new Vector3(playerBg.sizeDelta.x + 10, (playerXP.sizeDelta.y * 2), 0));
        Utility.SetScreenSizeUI(canvas, "PlayerStamina", Vector2.zero, Vector2.zero, new Vector3(0, 0f, 0));
        RectTransform playerHealth = Utility.SetScreenSizeUI(canvas, "PlayerHealth", Vector2.zero, Vector2.zero, new Vector3(0, 0, 0));
        Utility.SetScreenSizeUI(canvas, "PlayerItems", Vector2.zero, Vector2.zero, new Vector3(0, -(playerHealth.sizeDelta.y + 60), 0f));

        RectTransform playerItems = Utility.SetScreenSizeUI("PlayerItems", new Vector2(10, 15), new Vector3(0, -(playerHealth.sizeDelta.y + 60), 0f));
        playerItems.GetComponent<GridLayoutGroup>().cellSize = new Vector2((Screen.height / (3f * 7f)), (Screen.height / (3f * 7f)));
        RectTransform playerCurrnecy = Utility.SetScreenSizeUI("PlayerCurrency", new Vector2(10, 20), new Vector3(playerItems.sizeDelta.x, -(playerHealth.sizeDelta.y + 60), 0f));
        //Settings
        RectTransform currencyImage = Utility.SetScreenSizeUI("CurrencyImage", Vector2.zero, new Vector2(playerCurrnecy.sizeDelta.y, playerCurrnecy.sizeDelta.y), new Vector2(20f, 0f));
        // Utility.SetScreenSizeUI("CurrencyText", new Vector2(playerCurrnecy.sizeDelta.x - currencyImage.sizeDelta.x, playerCurrnecy.sizeDelta.y), Vector2.zero);
        Utility.FindUIObjectWithName("CurrencyText").GetComponent<RectTransform>().sizeDelta = new Vector2(playerCurrnecy.sizeDelta.x - currencyImage.sizeDelta.x, playerCurrnecy.sizeDelta.y);
        //Inventory  you need to repair all settings here.
        // 
        Utility.SetScreenSizeUI(canvas, "GeneralOptions", new Vector2(5f, 1), Vector2.zero, Vector3.zero);
        Utility.SetScreenSizeUI(canvas, "SettingContents", new Vector2(5, 6), Vector2.zero, Vector3.zero);
        Utility.SetScreenSizeUI(canvas, "OptionPanels", new Vector2(1.5f, 1), Vector2.zero, Vector3.zero);
        Utility.SetScreenSizeUI(canvas, "Inventory", new Vector2(2.5f, 1), Vector2.zero, Vector3.zero);
        Utility.FindGameObjectWithName(canvas, "InventoryItems").GetComponent<GridLayoutGroup>().cellSize = new Vector2((Screen.height / (3f * 3.5f)), (Screen.height / (3f * 3.5f)));
        Utility.SetScreenSizeUI(canvas, "InventoryItems", new Vector2(2.5f, 2.5f), Vector2.zero, new Vector3(0, 150f, 0));
        //        Utility.ScreenAuteSizeUI(canvas, "Fragments", new Vector2(2.5f, 4), Vector2.zero, new Vector3(0, -40f, 0));
        Utility.FindGameObjectWithName(canvas, "MainMenu").GetComponent<Button>().onClick.AddListener(() => Utility.LoadMe(1));
        // Utility.CreateButtonSetup(Utility.FindGameObjectWithName(canvas, "SystemInven"), "Inventory", "InventoryBack");

        //Aniamtor
        // resize objects too 
        Animator inventoryAniamtor = Utility.FindGameObjectWithName(canvas, "Inventory").GetComponent<Animator>();
        RectTransform InventoryItems = inventoryAniamtor.transform.Find("InventoryItems").GetComponent<RectTransform>();
        RectTransform GlanceSkills = inventoryAniamtor.transform.Find("GlanceSkills").GetComponent<RectTransform>();
        RectTransform GlanceLists = inventoryAniamtor.transform.Find("GlanceLists").GetComponent<RectTransform>();
        RectTransform healingPotion = inventoryAniamtor.transform.Find("HealingPotion").GetComponent<RectTransform>();
        GameObject SystemIven = Utility.FindGameObjectWithName(canvas, "SystemMenu");
        systemInven.GetComponent<Button>().onClick.AddListener(() =>
        {
            inventoryAniamtor.gameObject.SetActive(true);
            inventoryAniamtor.SetTrigger("OpenInventory");
            inventoryAniamtor.ResetTrigger("CloseInventory");

        });

        // Invnetory Panel
        GameObject Inventory = Utility.FindGameObjectWithName(canvas, "InventoryBack");
        //Inventory.GetComponent<RectTransform>().sizeDelta = new Vector2((Screen.width / 12f), (Screen.height / 12f));
        //Inventory.GetComponent<RectTransform>().anchoredPosition = new Vector2(-(Screen.width / 50f), (Screen.height / 28f));
        Utility.SetScreenSizeUI("InventoryBack", new Vector2((12f), (12f)), new Vector2(-(Screen.width / 50f), (Screen.height / 28f)));
        InventoryItems.anchoredPosition = new Vector2(0f, Inventory.GetComponent<RectTransform>().sizeDelta.y + Inventory.GetComponent<RectTransform>().anchoredPosition.y);
        InventoryItems.sizeDelta = new Vector2(InventoryItems.sizeDelta.x, (Screen.height / 3f));
        GlanceSkills.anchoredPosition = new Vector2(0f, Inventory.GetComponent<RectTransform>().sizeDelta.y + Inventory.GetComponent<RectTransform>().anchoredPosition.y + InventoryItems.sizeDelta.y);
        GlanceSkills.sizeDelta = new Vector2((InventoryItems.sizeDelta.x / 1.5f), (Screen.height / 6f));
        GlanceSkills.GetComponent<GridLayoutGroup>().cellSize = new Vector2((Screen.height / (3f * 3.5f)), (Screen.height / (3f * 3.5f)));

        healingPotion.anchoredPosition = new Vector2(0f, Inventory.GetComponent<RectTransform>().sizeDelta.y + Inventory.GetComponent<RectTransform>().anchoredPosition.y + InventoryItems.sizeDelta.y);
        healingPotion.sizeDelta = new Vector2((InventoryItems.sizeDelta.x / 3f), (Screen.height / 6f));
        healingPotion.GetComponent<GridLayoutGroup>().cellSize = new Vector2((Screen.height / (3f * 3.5f)), (Screen.height / (3f * 3.5f)));
        GlanceLists.anchoredPosition = new Vector2(0f, -((Inventory.GetComponent<RectTransform>().sizeDelta.y / 2)));
        GlanceLists.sizeDelta = new Vector2((InventoryItems.sizeDelta.x / 2), (Screen.height / 3f));
        GlanceLists.GetComponent<GridLayoutGroup>().cellSize = new Vector2((Screen.height / (3f * 3.5f)), (Screen.height / (3f * 3.5f)));
        Inventory.GetComponent<Button>().onClick.AddListener(() =>
        {
            inventoryAniamtor.SetTrigger("CloseInventory");
            inventoryAniamtor.ResetTrigger("OpenInventory");
        });
        // Settings Panel
        Utility.CreateButtonSetup(Utility.FindGameObjectWithName(canvas, "SystemMenu"), "Setting", "SettingsBack");



        //Upgradalbe Panel
        RectTransform UpgradeGlances = Utility.FindGameObjectWithName(canvas, "UpgradeManager").GetComponent<RectTransform>();
        RectTransform UpgradableLists = UpgradeGlances.Find("UpgradableLists").GetComponent<RectTransform>();
        RectTransform UpgradablePanel = UpgradeGlances.Find("UpgradablePanel").GetComponent<RectTransform>();
        // upgradable lists
        UpgradableLists.sizeDelta = new Vector2((InventoryItems.sizeDelta.x / 2), (Screen.height / 3f));
        UpgradableLists.GetComponent<GridLayoutGroup>().cellSize = new Vector2((Screen.height / (3f * 3.5f)), (Screen.height / (3f * 3.5f)));
        UpgradableLists.anchoredPosition = new Vector2(-20, -20);


        UpgradablePanel.sizeDelta = new Vector2((Screen.width / 3f), (Screen.height / 1.2f));
        RectTransform Glance = UpgradablePanel.Find("Glance").GetComponent<RectTransform>();
        RectTransform Upgrade = UpgradablePanel.Find("Upgrade").GetComponent<RectTransform>();
        RectTransform UpgradeInfo = UpgradablePanel.Find("UpgradeInfo").GetComponent<RectTransform>();
        RectTransform requirements = UpgradablePanel.Find("Requirements").GetComponent<RectTransform>();
        RectTransform upgradeExit = UpgradablePanel.Find("UpgradeExit").GetComponent<RectTransform>();


        Glance.sizeDelta = new Vector2((Screen.height / (3f * 2f)), (Screen.height / (3f * 2f)));
        Glance.anchoredPosition = new Vector2(0f, -((Glance.sizeDelta.y / 2)));

        Upgrade.sizeDelta = new Vector2((Upgrade.sizeDelta.x / 1.2f), (Screen.height / 14f));
        Upgrade.anchoredPosition = new Vector2(0f, 40f);

        UpgradeInfo.sizeDelta = new Vector2((UpgradablePanel.sizeDelta.x / 1.2f), (UpgradablePanel.sizeDelta.y / 4));
        UpgradeInfo.anchoredPosition = new Vector2(0f, Upgrade.sizeDelta.y + 40f + 10f);

        requirements.sizeDelta = new Vector2((UpgradablePanel.sizeDelta.x / 1.2f), (UpgradablePanel.sizeDelta.y / 4));
        requirements.anchoredPosition = new Vector2(0f, Upgrade.sizeDelta.y + UpgradeInfo.sizeDelta.y + 40f + 10f);

        upgradeExit.sizeDelta = new Vector2((Screen.height / (3f * 4f)), (Screen.height / (3f * 4f)));
        //     Utility.createPopUpMessage(true, canvas, "Canvas", Utility.PRESET.MIDDLE_CENTER,
        //    "Hello Player! welcome to the project Nemesis", "Collect Few boxes to gain item ability, you are welcome to use the controllers and action buttons.",
        //     new Vector2(3, 3), Vector2.zero, Vector3.zero);
        // Utility.ScreenAuteSizeUI(canvas, "BottomBar", new Vector2(0, 2), Vector2.zero, Vector3.zero);
        //Cinematic view.
        Utility.FindGameObjectWithName(canvas, "TopBar").GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width, (Screen.height / 4f));
        Utility.FindGameObjectWithName(canvas, "BottomBar").GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width, (Screen.height / 4f));
        ShowGamePlay();
    }

    public void ShowGamePlay()
    {
        userPanel.ResetTrigger("FadeOut");
        cinematicPanel.ResetTrigger("FadeIn");
        userPanel.SetTrigger("FadeIn");

        cinematicPanel.SetTrigger("FadeOut");

    }

    public void ShowCinematic()
    {
        userPanel.ResetTrigger("FadeIn");
        cinematicPanel.ResetTrigger("FadeOut");
        userPanel.SetTrigger("FadeOut");
        cinematicPanel.SetTrigger("FadeIn");

    }
    void LateUpdate()
    {
        Utility.setSliderUI(GameObject.Find("Canvas"), "Slider", 0, 1, SystemInfo.batteryLevel, false);
        float avgFrameRate = Time.frameCount / Time.time;
        //textTime.text = System.DateTime.UtcNow.ToLocalTime().ToString();
        textTime.text = avgFrameRate.ToString("000") + "FPS";
        if (Input.GetKeyDown(KeyCode.U))
        {
            ShowCinematic();
            //   FindObjectOfType<DialogueInterractible>().InteractDialogue();
            FindObjectOfType<DialogueTask>().StartDialogue();
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            ShowGamePlay();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            FindObjectOfType<ConversationManager>().StartConversationTask(1);
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
