using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour  // <--- ĐÂY LÀ DÒNG QUAN TRỌNG
{
    public Canvas GameMenu;
    private bool isShowingMenu = false;
    
    public TMP_Text Title;
    public TMP_Text Message;
    public Button ContinueButton;
    public Button RestartButton;
    
    [Header("Test Settings")]
    public bool showMenuOnStart = true; // Tự động hiển thị menu khi Start

    void Start()
    {
        // Kiểm tra và khởi tạo GameMenu
        if (GameMenu != null)
        {
            // Nếu showMenuOnStart = true, hiển thị menu ngay
            if (showMenuOnStart)
            {
                ShowMenu();
            }
            else
            {
                GameMenu.gameObject.SetActive(isShowingMenu);
            }
        }
        else
        {
            Debug.LogWarning("GameMenu chưa được gán trong Inspector!");
            // Tự động tạo Canvas nếu chưa có
            CreateGameMenu();
        }
        
        // Kiểm tra và thiết lập UI elements
        if (Title != null)
        {
            Title.text = "Welcome to Unity!";
        }
        
        if (Message != null)
        {
            Message.text = "Do you want continue?";
        }
        
        if (ContinueButton != null)
        {
            var continueText = ContinueButton.GetComponentInChildren<TMP_Text>();
            if (continueText != null)
            {
                continueText.text = "Enter";
            }
        }
        
        if (RestartButton != null)
        {
            var restartText = RestartButton.GetComponentInChildren<TMP_Text>();
            if (restartText != null)
            {
                restartText.text = "Restart";
            }
        }
        
        Debug.Log("GameManager đã khởi động!");
    }

    // Hàm hiển thị menu
    public void ShowMenu()
    {
        if (GameMenu != null)
        {
            isShowingMenu = true;
            GameMenu.gameObject.SetActive(true);
            Debug.Log("Menu đã được hiển thị!");
        }
    }

    // Hàm ẩn menu
    public void HideMenu()
    {
        if (GameMenu != null)
        {
            isShowingMenu = false;
            GameMenu.gameObject.SetActive(false);
            Debug.Log("Menu đã được ẩn!");
        }
    }

    // Tự động tạo GameMenu nếu chưa có
    void CreateGameMenu()
    {
        Debug.Log("Đang tạo GameMenu tự động...");
        
        // Tạo Canvas
        GameObject canvasObj = new GameObject("GameMenu");
        Canvas canvas = canvasObj.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvasObj.AddComponent<CanvasScaler>();
        canvasObj.AddComponent<GraphicRaycaster>();
        
        GameMenu = canvas;
        
        // Tạo Panel background
        GameObject panelObj = new GameObject("Panel");
        panelObj.transform.SetParent(canvasObj.transform, false);
        RectTransform panelRect = panelObj.AddComponent<RectTransform>();
        panelRect.anchorMin = Vector2.zero;
        panelRect.anchorMax = Vector2.one;
        panelRect.sizeDelta = Vector2.zero;
        panelObj.AddComponent<UnityEngine.UI.Image>().color = new Color(0, 0, 0, 0.8f);
        
        // Tạo Title
        GameObject titleObj = new GameObject("Title");
        titleObj.transform.SetParent(panelObj.transform, false);
        Title = titleObj.AddComponent<TextMeshProUGUI>();
        Title.text = "Welcome to Unity!";
        Title.fontSize = 48;
        Title.alignment = TextAlignmentOptions.Center;
        RectTransform titleRect = titleObj.GetComponent<RectTransform>();
        titleRect.anchorMin = new Vector2(0.5f, 0.7f);
        titleRect.anchorMax = new Vector2(0.5f, 0.7f);
        titleRect.anchoredPosition = Vector2.zero;
        titleRect.sizeDelta = new Vector2(400, 60);
        
        // Tạo Message
        GameObject msgObj = new GameObject("Message");
        msgObj.transform.SetParent(panelObj.transform, false);
        Message = msgObj.AddComponent<TextMeshProUGUI>();
        Message.text = "Do you want continue?";
        Message.fontSize = 32;
        Message.alignment = TextAlignmentOptions.Center;
        RectTransform msgRect = msgObj.GetComponent<RectTransform>();
        msgRect.anchorMin = new Vector2(0.5f, 0.5f);
        msgRect.anchorMax = new Vector2(0.5f, 0.5f);
        msgRect.anchoredPosition = Vector2.zero;
        msgRect.sizeDelta = new Vector2(400, 40);
        
        // Tạo Continue Button
        GameObject continueBtnObj = new GameObject("ContinueButton");
        continueBtnObj.transform.SetParent(panelObj.transform, false);
        ContinueButton = continueBtnObj.AddComponent<Button>();
        continueBtnObj.AddComponent<Image>().color = Color.green;
        RectTransform continueRect = continueBtnObj.GetComponent<RectTransform>();
        continueRect.anchorMin = new Vector2(0.5f, 0.3f);
        continueRect.anchorMax = new Vector2(0.5f, 0.3f);
        continueRect.anchoredPosition = new Vector2(-100, 0);
        continueRect.sizeDelta = new Vector2(150, 50);
        
        GameObject continueTextObj = new GameObject("Text");
        continueTextObj.transform.SetParent(continueBtnObj.transform, false);
        TMP_Text continueText = continueTextObj.AddComponent<TextMeshProUGUI>();
        continueText.text = "Enter";
        continueText.fontSize = 24;
        continueText.alignment = TextAlignmentOptions.Center;
        continueText.color = Color.white;
        RectTransform continueTextRect = continueTextObj.GetComponent<RectTransform>();
        continueTextRect.anchorMin = Vector2.zero;
        continueTextRect.anchorMax = Vector2.one;
        continueTextRect.sizeDelta = Vector2.zero;
        
        ContinueButton.onClick.AddListener(NextScene);
        
        // Tạo Restart Button
        GameObject restartBtnObj = new GameObject("RestartButton");
        restartBtnObj.transform.SetParent(panelObj.transform, false);
        RestartButton = restartBtnObj.AddComponent<Button>();
        restartBtnObj.AddComponent<Image>().color = Color.red;
        RectTransform restartRect = restartBtnObj.GetComponent<RectTransform>();
        restartRect.anchorMin = new Vector2(0.5f, 0.3f);
        restartRect.anchorMax = new Vector2(0.5f, 0.3f);
        restartRect.anchoredPosition = new Vector2(100, 0);
        restartRect.sizeDelta = new Vector2(150, 50);
        
        GameObject restartTextObj = new GameObject("Text");
        restartTextObj.transform.SetParent(restartBtnObj.transform, false);
        TMP_Text restartText = restartTextObj.AddComponent<TextMeshProUGUI>();
        restartText.text = "Restart";
        restartText.fontSize = 24;
        restartText.alignment = TextAlignmentOptions.Center;
        restartText.color = Color.white;
        RectTransform restartTextRect = restartTextObj.GetComponent<RectTransform>();
        restartTextRect.anchorMin = Vector2.zero;
        restartTextRect.anchorMax = Vector2.one;
        restartTextRect.sizeDelta = Vector2.zero;
        
        RestartButton.onClick.AddListener(RestartScene);
        
        // Hiển thị menu
        if (showMenuOnStart)
        {
            ShowMenu();
        }
        
        Debug.Log("GameMenu đã được tạo tự động!");
    }

    public void NextScene()
    {
        // Kiểm tra scene có tồn tại không
        if (Application.CanStreamedLevelBeLoaded("_Scene_1"))
        {
            SceneManager.LoadScene("_Scene_1");
        }
        else
        {
            Debug.LogWarning("Scene '_Scene_1' không tồn tại! Quay lại SampleScene.");
            SceneManager.LoadScene("SampleScene");
        }
    }

    public void RestartScene()
    {
        // Kiểm tra scene có tồn tại không
        if (Application.CanStreamedLevelBeLoaded("_Scene_3_2"))
        {
            SceneManager.LoadScene("_Scene_3_2");
        }
        else
        {
            Debug.LogWarning("Scene '_Scene_3_2' không tồn tại! Quay lại SampleScene.");
            SceneManager.LoadScene("SampleScene");
        }
    }
}