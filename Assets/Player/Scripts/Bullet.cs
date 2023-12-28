using UnityEngine;

public class Bullet : MonoBehaviour
{
    private PlayAreaClamp playAreaClamp;

    void Start()
    {
        playAreaClamp = FindObjectOfType<PlayAreaClamp>();
    }

    void Update()
    {
        CheckBounds();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Logic for hitting an enemy
            Destroy(gameObject);
        }
    }

    private void CheckBounds()
    {
        if (playAreaClamp != null)
        {
            Vector2 minClamp = playAreaClamp.minClamp;
            Vector2 maxClamp = playAreaClamp.maxClamp;

            if (transform.position.x < minClamp.x - 3 || transform.position.x > maxClamp.x + 3 ||
                transform.position.y < minClamp.y - 3 || transform.position.y > maxClamp.y + 3)
            {
                Destroy(gameObject);
            }
        }
    }
}
