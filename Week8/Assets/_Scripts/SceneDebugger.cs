using UnityEngine;
using TMPro;

public class SceneDebugger : MonoBehaviour
{
    public TMP_Text debugText;
    private GameObject debugTextObject;

    void Start()
    {
        // Tạo debug text nếu chưa có
        if (debugText == null)
        {
            CreateDebugText();
        }

        UpdateDebugInfo();
    }

    void CreateDebugText()
    {
        // Tạo Canvas nếu chưa có
        Canvas canvas = FindObjectOfType<Canvas>();
        if (canvas == null)
        {
            GameObject canvasObj = new GameObject("DebugCanvas");
            canvas = canvasObj.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvasObj.AddComponent<UnityEngine.UI.CanvasScaler>();
            canvasObj.AddComponent<UnityEngine.UI.GraphicRaycaster>();
        }

        // Tạo Text object
        GameObject textObj = new GameObject("DebugText");
        textObj.transform.SetParent(canvas.transform, false);
        
        debugText = textObj.AddComponent<TextMeshProUGUI>();
        debugText.fontSize = 24;
        debugText.color = Color.white;
        
        RectTransform rectTransform = textObj.GetComponent<RectTransform>();
        rectTransform.anchorMin = new Vector2(0, 1);
        rectTransform.anchorMax = new Vector2(0, 1);
        rectTransform.pivot = new Vector2(0, 1);
        rectTransform.anchoredPosition = new Vector2(10, -10);
        rectTransform.sizeDelta = new Vector2(500, 200);

        debugTextObject = textObj;
    }

    void UpdateDebugInfo()
    {
        if (debugText != null)
        {
            int objectCount = FindObjectsOfType<GameObject>().Length;
            int cameraCount = FindObjectsOfType<Camera>().Length;
            
            debugText.text = $"<b>Scene Debug Info</b>\n" +
                           $"Scene: {UnityEngine.SceneManagement.SceneManager.GetActiveScene().name}\n" +
                           $"Game Objects: {objectCount}\n" +
                           $"Cameras: {cameraCount}\n" +
                           $"GameManager: {(FindObjectOfType<GameManager>() != null ? "Có" : "Không")}\n" +
                           $"EnterGame: {(FindObjectOfType<EnterGame>() != null ? "Có" : "Không")}";
        }
    }

    void Update()
    {
        // Cập nhật mỗi giây
        if (Time.frameCount % 60 == 0)
        {
            UpdateDebugInfo();
        }
    }
}

