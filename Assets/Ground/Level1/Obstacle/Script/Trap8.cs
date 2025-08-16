using UnityEngine;

public class Trap8 : MonoBehaviour
{
    public Transform rotatePoint;
    public GameObject tree;
    public float fallAngle = 90f; // Ke kanan
    public float fallSpeed = 90f; // Derajat per detik

    private bool isFalling = false;
    private float currentAngle = 0f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isFalling = true;
            tree.gameObject.tag = "Enemy";
        }
    }
    private void Update()
    {
        if (isFalling && currentAngle < fallAngle)
        {
            float deltaAngle = fallSpeed * Time.deltaTime;
            float angleToRotate = Mathf.Min(deltaAngle, fallAngle - currentAngle);
            rotatePoint.Rotate(0f, 0f, -angleToRotate);
            currentAngle += angleToRotate;
        }
    }
}
