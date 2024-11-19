using Mirror;
using UnityEngine;

public class Player : NetworkBehaviour
{
    public float speed = 5f; // Tốc độ di chuyển
    private Rigidbody2D rb;  // Tham chiếu đến Rigidbody2D

    void Start()
    {
        if (isLocalPlayer)
        {
            // Lấy tham chiếu đến Rigidbody2D nếu là player local
            rb = GetComponent<Rigidbody2D>();

            // Tắt trọng lực trên Rigidbody2D của player
            if (rb != null)
            {
                rb.gravityScale = 0;  // Tắt trọng lực
            }
        }
    }

    void Update()
    {
        if (isLocalPlayer)
        {
            HandleMovement();
        }
    }

    // Xử lý di chuyển player
    public void HandleMovement()
    {
        float ipVertical = Input.GetAxis("Vertical");   // W/S hoặc Arrow Keys
        float ipHorizontal = Input.GetAxis("Horizontal"); // A/D hoặc Arrow Keys

        // Tạo vector di chuyển (theo chiều ngang và dọc)
        Vector2 movement = new Vector2(ipHorizontal, ipVertical).normalized * speed * Time.deltaTime;

        // Di chuyển player sử dụng Rigidbody2D
        if (rb != null)
        {
            rb.MovePosition(rb.position + movement);  // Dùng MovePosition để di chuyển player
        }
    }
}
