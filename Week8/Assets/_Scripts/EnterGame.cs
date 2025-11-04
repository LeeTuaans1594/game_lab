using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterGame : MonoBehaviour
{
    public Canvas GameMenu;

    private void OnTriggerEnter(Collider other)
    {
        // Đảm bảo máy bay của bạn có tag "Player"
        if (other.CompareTag("Player"))
        {
            // SceneManager.LoadScene("_Scene_1"); // Vô hiệu hóa dòng này
            GameMenu.enabled = true; // Kích hoạt pop-up
        }
    }
}