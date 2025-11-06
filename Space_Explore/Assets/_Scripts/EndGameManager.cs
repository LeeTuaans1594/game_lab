using UnityEngine;
using UnityEngine.SceneManagement; // BẮT BUỘC [15, 1, 2]
using TMPro; // BẮT BUỘC [9, 16]

/// <summary>
/// Quản lý Scene EndGame, hiển thị điểm và các nút điều hướng.
/// </summary>
public class EndGameManager : MonoBehaviour
{
    public TMP_Text finalScoreText;

    void Start()
    {
        // Lấy điểm số đã được GameManager lưu trữ
        int finalScore = PlayerPrefs.GetInt("FinalScore", 0); // [6, 17]
        
        // Hiển thị điểm số lên UI
        if (finalScoreText!= null)
        {
            finalScoreText.SetText("Score: " + finalScore.ToString()); // [18, 16]
        }
    }

    /// <summary>
    /// Tải lại Scene Gameplay. Được gọi bởi ReplayButton.
    /// </summary>
    public void ReplayGame()
    {
        // Đảm bảo bỏ pause nếu trước đó Time.timeScale = 0
        Time.timeScale = 1f;

        // Tải lại Scene "GameplayScene" [1, 19, 2]
        SceneManager.LoadScene("GameplayScene"); 
    }

    /// <summary>
    /// Quay về Main Menu. Được gọi bởi MenuButton.
    /// </summary>
    public void GoToMenu()
    {
        SceneManager.LoadScene("MainMenuScene"); // [15, 1, 2]
    }

    /// <summary>
    /// Thoát game. Được gọi bởi QuitButton.
    /// </summary>
    public void QuitGame()
    {
        Debug.Log("QUIT GAME!"); // Để kiểm tra trong Console

        #if UNITY_EDITOR
            // Dừng game trong Editor
            UnityEditor.EditorApplication.isPlaying = false; // [20, 21]
        #else
            // Thoát game trong bản build (file.exe)
            Application.Quit(); // [22, 23, 21]
        #endif
    }
}