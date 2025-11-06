using UnityEngine;

public class AsteroidMovement : MonoBehaviour
{
    public float minSpeed = 1f;
    public float maxSpeed = 3f;
    private Rigidbody2D rb;

    // Biến này sẽ nhận giá trị từ GameManager (mặc định là 1)
    private float speedMultiplier = 1.0f;

    /// <summary>
    /// Hàm MỚI: GameManager sẽ gọi hàm này ngay khi tạo ra thiên thạch
    /// để gửi hệ số tốc độ (dựa trên điểm) vào.
    /// </summary>
    public void SetSpeedMultiplier(float multiplier)
    {
        speedMultiplier = multiplier;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        // --- SỬA LỖI HƯỚNG BAY ---
        // Tạo một hướng ngẫu nhiên nhưng LUÔN có thành phần Y âm (bay xuống)
        float dirX = Random.Range(-0.8f, 0.8f); // Hơi trái hoặc hơi phải
        float dirY = Random.Range(-0.5f, -1.0f); // Luôn bay xuống
        Vector2 randomDirection = new Vector2(dirX, dirY).normalized; // [5, 6, 7]

        // --- NÂNG CẤP TỐC ĐỘ ---
        // Tính tốc độ ngẫu nhiên VÀ nhân với hệ số tốc độ từ GameManager
        float randomSpeed = Random.Range(minSpeed, maxSpeed) * speedMultiplier;
        
        // Đẩy thiên thạch đi
        rb.AddForce(randomDirection * randomSpeed, ForceMode2D.Impulse); // 
    }
}