using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 movement;
    private PlayAreaClamp playAreaClamp;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playAreaClamp = FindAnyObjectByType<PlayAreaClamp>();
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement.Normalize();
    }

    void FixedUpdate()
    {
        Vector2 newPosition = rb.position + movement * moveSpeed * Time.fixedDeltaTime;
        newPosition = playAreaClamp.ClampPosition(newPosition);
        rb.MovePosition(newPosition);
    }
}
