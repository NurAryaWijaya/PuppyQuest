using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    public float speed = 2f;
    private float leftLimit;
    private float rightLimit;
    private bool movingleft = true; // mulai ke kanan

    void Start()
    {
        Vector2 startPos = transform.position;
        leftLimit = startPos.x - 5f;
        rightLimit = startPos.x + 5f;
    }

    void Update()
    {
        // Gerak sesuai arah
        if (movingleft)
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
            if (transform.position.x <= leftLimit)
            {
                movingleft = false;
                Flip();
            }
        }
        else
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
            if (transform.position.x >= rightLimit)
            {
                movingleft = true;
                Flip();
            }
        }
    }

    void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
