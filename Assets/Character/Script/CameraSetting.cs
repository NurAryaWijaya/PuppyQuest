using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraSetting : MonoBehaviour
{
    public Transform player;          // Referensi ke player
    public float leftOffset = 2f;     // Offset kiri
    public float rightOffset = 2f;    // Offset kanan
    public float smoothSpeed = 5f;    // Kecepatan kamera bergerak

    public float minX = 0f;
    public float maxX = 20f;

    private float targetX;
    void LateUpdate()
    {
        if (player == null) return;

        float cameraX = transform.position.x;
        float playerX = player.position.x;

        float leftBound = cameraX - leftOffset;
        float rightBound = cameraX + rightOffset;

        if (playerX < leftBound)
        {
            targetX = playerX + leftOffset;
        }
        else if (playerX > rightBound)
        {
            targetX = playerX - rightOffset;
        }
        else
        {
            targetX = cameraX;
        }

        // ✅ Clamp posisi X agar tidak keluar dari batas level
        targetX = Mathf.Clamp(targetX, minX, maxX);

        Vector3 targetPosition = new Vector3(targetX, transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * smoothSpeed);
    }

}
