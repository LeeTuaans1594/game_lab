using System.Collections;
using UnityEngine;
using TMPro; // BẮT BUỘC để dùng TextMeshPro [1, 9, 11, 12, 13, 14, 7, 15, 16]
using UnityEngine.SceneManagement; // BẮT BUỘC để quản lý Scene [17, 2, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27]

/// <summary>
/// Quản lý điểm số, UI, và spawn vật thể.
/// (Đã cập nhật logic tăng độ khó)
/// </summary>
public class GameManager : MonoBehaviour
{
    [Header("Game Objects")]
    public GameObject[] asteroidPrefabs; // Mảng (Array) các thiên thạch
    public GameObject starPrefab;

    [Header("UI Elements")]
    public TMP_Text scoreText; // [1, 9, 11, 12, 13, 14, 7, 15, 16]

    [Header("Asteroid Logic")]
    public float asteroidSpawnInterval = 3f; // Tốc độ spawn ban đầu (3 giây)
    public float minSpawnInterval = 0.3f; // Giới hạn nhanh nhất (như bạn set)

   
    public float starSpawnInterval = 7f; // Tốc độ spawn sao (chậm hơn)

    private int score = 0;

    void Start()
    {
        score = 0;
        UpdateScoreText(); // Cập nhật UI lúc bắt đầu [1, 11, 12, 13, 36]
        
        // Bắt đầu 2 Coroutine (vòng lặp) spawn riêng biệt
        StartCoroutine(SpawnAsteroidRoutine()); // [3, 4, 29, 31, 5, 6, 32, 7, 8, 37, 38]
        StartCoroutine(SpawnStarRoutine()); // [3, 4, 29, 31, 5, 6, 32, 7, 8, 37, 38]
    }

    /// <summary>
    /// Coroutine chỉ để spawn THIÊN THẠCH (với độ khó tăng dần)
    /// </summary>
    IEnumerator SpawnAsteroidRoutine()
    {
        while (true) 
        {
            // --- NÂNG CẤP: TÍNH TOÁN ĐỘ KHÓ (THEO YÊU CẦU MỚI) ---
            // Cứ 30 điểm, giảm 0.3 giây
            float currentInterval = asteroidSpawnInterval - ((score / 30f) * 0.3f); // [1]

            // Giới hạn, không cho phép spawn nhanh hơn 0.3 giây (như bạn set)
            if (currentInterval < minSpawnInterval)
            {
                currentInterval = minSpawnInterval;
            }
            // ------------------------------------

            // 1. Chờ N giây (theo thời gian đã tính toán)
            yield return new WaitForSeconds(currentInterval); // [3, 4, 29, 31, 5, 6, 7, 8]
            
            // --- LOGIC SPAWN THIÊN THẠCH NGẪU NHIÊN ---
            if (asteroidPrefabs.Length > 0) 
            {
                int randomIndex = Random.Range(0, asteroidPrefabs.Length); // [29, 35]
                GameObject randomAsteroidPrefab = asteroidPrefabs[randomIndex];
                
                Vector2 spawnPosAsteroid = new Vector2(Random.Range(-8f, 8f), 7f); // Spawn từ trên cùng [29]
                
                // 1. Tạo thiên thạch VÀ giữ một tham chiếu đến nó
                GameObject newAsteroid = Instantiate(randomAsteroidPrefab, spawnPosAsteroid, Quaternion.identity); // [39, 40, 41, 29, 42, 43, 44, 45, 46, 47, 48, 49, 50, 51, 52, 33, 53]
                
                // 2. Lấy script AsteroidMovement từ thiên thạch vừa tạo
                AsteroidMovement movementScript = newAsteroid.GetComponent<AsteroidMovement>();
                
                if (movementScript!= null)
                {
                    // 3. Tính toán hệ số tốc độ (tăng 20% tốc độ mỗi 100 điểm)
                    float speedMultiplier = 1.0f + (score / 100f) * 0.2f; // [1]
                    
                    // 4. Gửi hệ số này vào script của thiên thạch
                    movementScript.SetSpeedMultiplier(speedMultiplier);
                }
            }
        }
    }

    /// <summary>
    /// Coroutine THỨ HAI: chỉ để spawn SAO (với tốc độ cố định)
    /// </summary>
    IEnumerator SpawnStarRoutine()
    {
        while (true)
        {
            // Chờ theo thời gian spawn sao (mà bạn có thể chỉnh)
            yield return new WaitForSeconds(starSpawnInterval); // [3, 4, 29, 31, 5, 6, 7, 8]

            if(starPrefab!= null)
            {
                // Spawn ngẫu nhiên BÊN TRONG màn hình
                Vector2 spawnPosStar = new Vector2(Random.Range(-8f, 8f), Random.Range(-4f, 4f));
                Instantiate(starPrefab, spawnPosStar, Quaternion.identity); // [39, 40, 41, 29, 42, 43, 44, 45, 46, 47, 48, 49, 50, 51, 52, 33, 53]
            }
        }
    }
    
    public void AddScore(int pointsToAdd)
    {
        score += pointsToAdd;
        UpdateScoreText(); // [1, 11, 12, 13, 36]
    }

    void UpdateScoreText()
    {
        if (scoreText!= null)
        {
            scoreText.SetText("Score: " + score.ToString()); // [1, 9, 11, 12, 13, 14, 7, 15, 16]
        }
    }

    public void GameOver()
    {
        PlayerPrefs.SetInt("FinalScore", score); // [54, 55, 56, 57, 58]
        PlayerPrefs.Save(); // [54]
        SceneManager.LoadScene("EndGameScene"); // [17, 2, 18, 19, 59, 20, 21, 22, 23, 55, 24, 25, 26, 27, 57, 58]
    }
}