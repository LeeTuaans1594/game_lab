using UnityEngine;
using UnityEngine.SceneManagement; // BẮT BUỘC [1, 8, 9]

/// <summary>
/// Quản lý các nút bấm và UI trong MainMenuScene.
/// (Đã cập nhật thêm nút Quit)
/// </summary>
public class MainMenuManager : MonoBehaviour
{
    // Kéo Panel Hướng dẫn vào đây trong Inspector
    public GameObject instructionsPanel;

    /// <summary>
    /// Được gọi bởi PlayButton.
    /// </summary>
    public void PlayGame()
    {
        // Tải Scene Gameplay. Tên "GameplayScene" phải KHỚP CHÍNH XÁC
        SceneManager.LoadScene("GameplayScene"); // [1, 9]
    }

    /// <summary>
    /// Được gọi bởi InstructionsButton.
    /// </summary>
    public void ShowInstructionsPanel()
    {
        // Kích hoạt (hiển thị) panel
        if (instructionsPanel!= null)
        {
            instructionsPanel.SetActive(true); // [10, 11, 12, 13, 14, 15, 16]
        }
    }

    /// <summary>
    /// Được gọi bởi CloseButton.
    /// </summary>
    public void HideInstructionsPanel()
    {
        // Hủy kích hoạt (ẩn) panel
        if (instructionsPanel!= null)
        {
            instructionsPanel.SetActive(false); // [10, 11, 12, 13, 14, 15, 16]
        }
    }

    /// <summary>
    /// HÀM MỚI: Được gọi bởi ExitButton.
    /// </summary>
    public void QuitGame()
    {
        Debug.Log("QUIT GAME!"); // Để kiểm tra trong Console [2]

        // Mã này xử lý cả 2 trường hợp:
        #if UNITY_EDITOR
            // Dừng game trong Editor
            UnityEditor.EditorApplication.isPlaying = false; // [3, 4, 6, 17]
        #else
            // Thoát game trong bản build (file.exe)
            Application.Quit(); // [2, 3, 4, 18, 5, 6, 17, 7]
        #endif
    }
}