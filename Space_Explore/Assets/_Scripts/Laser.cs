using UnityEngine;

/// <summary>
/// Quản lý hành vi của đạn Laser.
/// Bao gồm di chuyển (Phần 4) và logic va chạm (Phần 6).
/// </summary>
public class Laser : MonoBehaviour
{
    public float speed = 10f;

    // Update được gọi mỗi khung hình
    void Update()
    {
        // Di chuyển đạn thẳng lên trên [1]
        transform.Translate(Vector3.up * speed * Time.deltaTime); 
        
        // Tự hủy nếu bay ra khỏi màn hình (ví dụ: Y > 6) [1]
        if (transform.position.y > 6f) 
        {
            Destroy(this.gameObject); // [1, 2]
        }
    }

    // ------------------------------------------------------------------
    // PHẦN 6: XỬ LÝ VA CHẠM (ĐÃ BAO GỒM)
    // ------------------------------------------------------------------

    /// <summary>
    /// Xử lý khi đạn va chạm với vật thể khác
    /// </summary>
    private void OnCollisionEnter2D(Collision2D collision) // [3, 4, 5]
    {
        // Kiểm tra xem có phải là "Asteroid" không
        if (collision.gameObject.CompareTag("Asteroid"))
        {
            Destroy(collision.gameObject); // Hủy thiên thạch [2]
        }
        
        // Hủy chính viên đạn (bất kể nó chạm vào cái gì)
        Destroy(gameObject); // [2]
    }
}