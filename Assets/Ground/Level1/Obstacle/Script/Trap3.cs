using UnityEngine;

public class Trap3 : MonoBehaviour
{
    public GameObject platformTile;
    public float moveSpeed = 4f;
    public float tileSize = 1f; // Ukuran 1 tile, misalnya 1 unit
    private bool hasMovedFirst = false;
    private bool hasMovedSecond = false;

    public void MovePlatformDown(float tileCount, bool destroyAfter)
    {
        if (platformTile != null)
        {
            StartCoroutine(MoveDownRoutine(tileCount, destroyAfter));
        }
    }

    private System.Collections.IEnumerator MoveDownRoutine(float tileCount, bool destroyAfter)
    {
        Vector3 startPos = platformTile.transform.position;
        Vector3 targetPos = startPos + Vector3.down * tileCount * tileSize;

        while (Vector3.Distance(platformTile.transform.position, targetPos) > 0.01f)
        {
            platformTile.transform.position = Vector3.MoveTowards(
                platformTile.transform.position,
                targetPos,
                moveSpeed * Time.deltaTime
            );
            yield return null;
        }

        if (destroyAfter)
        {
            Destroy(platformTile);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        string triggerName = collision.gameObject.name;

        if (!hasMovedFirst && collision == transform.Find("TriggerBox1").GetComponent<Collider2D>())
        {
            hasMovedFirst = true;
            MovePlatformDown(4f, false);
        }
        else if (!hasMovedSecond && collision == transform.Find("TriggerBox2").GetComponent<Collider2D>())
        {
            hasMovedSecond = true;
            MovePlatformDown(10f, true);
        }
    }
}
