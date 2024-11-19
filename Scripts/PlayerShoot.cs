using Mirror;
using UnityEngine;

public class PlayerShoor : NetworkBehaviour
{
    public float lifetime = 5f; // Thời gian sống của viên đạn
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (isServer) // Duy trì viên đạn tồn tại trên server
        {
            Destroy(gameObject, lifetime); // Hủy viên đạn sau lifetime giây
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Khi viên đạn va chạm với vật thể khác
        if (isServer)
        {
            Destroy(gameObject); // Hủy viên đạn khi va chạm
        }
    }
}
