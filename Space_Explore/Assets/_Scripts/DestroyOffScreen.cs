using UnityEngine;

/// <summary>
/// Script này sẽ tự động hủy GameObject
/// khi nó bay ra khỏi 3 cạnh (DƯỚI, TRÁI, PHẢI).
/// Nó sẽ KHÔNG hủy nếu vật thể bay ra khỏi cạnh TRÊN.
/// </summary>
public class DestroyOffScreen : MonoBehaviour
{
    // Đặt ranh giới an toàn
    private float bottomBound = -8f;
    private float leftBound = -10f;
    private float rightBound = 10f;

    // Update được gọi mỗi khung hình
    void Update()
    {
        // THAY ĐỔI QUAN TRỌNG:
        // Chúng ta đã XÓA điều kiện (transform.position.y > topBound)
        // để cho phép vật thể spawn từ trên cùng bay vào.
        
        // Chỉ kiểm tra 3 cạnh
        if (transform.position.y < bottomBound || 
            transform.position.x < leftBound || 
            transform.position.x > rightBound)
        {
            Destroy(gameObject); // 
        }
    }
}
