using UnityEngine;
using UnityEngine.SceneManagement; 

/// <summary>
/// Quản lý di chuyển, bắn, và xử lý va chạm cho Player.
/// (Đã cập nhật FindObjectOfType thành FindFirstObjectByType)
/// </summary>
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f; // [3]
    private Rigidbody2D rb;
    private Vector2 moveInput;

   
    public GameObject laserPrefab; // [4, 5]
    public Transform firePoint; 

    [Header("Game Logic")]
    private GameManager gameManager; 

    // --- BIẾN MỚI ĐỂ GIỚI HẠN MÀN HÌNH ---
    private float minX, maxX, minY, maxY;
    private float shipPadding = 0.5f; 

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // [3]
        
        // --- SỬA LỖI CẢNH BÁO: Đổi FindObjectOfType thành FindFirstObjectByType ---
        gameManager = FindFirstObjectByType<GameManager>();

        // --- CODE MỚI: TÍNH TOÁN RANH GIỚI MÀN HÌNH ---
        Camera mainCamera = Camera.main;
        minX = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + shipPadding;
        minY = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + shipPadding;
        maxX = mainCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - shipPadding;
        maxY = mainCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - shipPadding;
    }

    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal"); // [3]
        moveInput.y = Input.GetAxisRaw("Vertical"); // [3]
        moveInput.Normalize(); // [3]

        if (Input.GetKeyDown(KeyCode.Space)) // [4, 5]
        {
            Shoot();
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = moveInput * moveSpeed; // [6, 3]
    }

    // --- HÀM MỚI ĐỂ GIỚI HẠN MÀN HÌNH ---
    void LateUpdate()
    {
        Vector2 clampedPos = transform.position;
        clampedPos.x = Mathf.Clamp(clampedPos.x, minX, maxX);
        clampedPos.y = Mathf.Clamp(clampedPos.y, minY, maxY);
        transform.position = clampedPos;
    }

    void Shoot()
    {
        if (laserPrefab!= null && firePoint!= null)
        {
            Instantiate(laserPrefab, firePoint.position, Quaternion.identity); // [4, 5]
        }
    }

    // PHẦN 6: XỬ LÝ VA CHẠM
    
    private void OnTriggerEnter2D(Collider2D other) // [7, 8]
    {
        if (other.CompareTag("Star"))
        {
            if (gameManager!= null)
            {
                gameManager.AddScore(10); // [9]
            }
            Destroy(other.gameObject); // [10]
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) // [11, 12, 13]
    {
        if (collision.gameObject.CompareTag("Asteroid"))
        {
            if (gameManager!= null)
            {
                gameManager.GameOver(); // [12, 13]
            }
            Destroy(gameObject); // [10, 14]
        }
    }
}