using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 movement;
    private PlayAreaClamp playAreaClamp;
    private float _playerHalfWidth;
    private float _playerHalfHeight;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playAreaClamp = FindObjectOfType<PlayAreaClamp>();
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        _playerHalfWidth = spriteRenderer.bounds.size.x / 2;
        _playerHalfHeight = spriteRenderer.bounds.size.y / 2;
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

        float clampedX = Mathf.Clamp(newPosition.x,
                                     playAreaClamp.minClamp.x + _playerHalfWidth,
                                     playAreaClamp.maxClamp.x - _playerHalfWidth);

        float clampedY = Mathf.Clamp(newPosition.y,
                                     playAreaClamp.minClamp.y + _playerHalfHeight,
                                     playAreaClamp.maxClamp.y - _playerHalfHeight);

        newPosition = new Vector3(clampedX, clampedY);
        rb.MovePosition(newPosition);
    }
}
