using UnityEngine;

public class Trap6 : MonoBehaviour
{
    public GameObject targetObject;
    public float moveDistance = 12f;
    public float moveSpeed = 20f;

    private bool hasTrigger = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        hasTrigger = true;
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Berhasil Terigger");
            StartCoroutine(ShootDown());
        }
    }

    private System.Collections.IEnumerator ShootDown()
    {
        Vector3 stratPost = targetObject.transform.position;
        Vector3 targetPost = stratPost + Vector3.down * moveDistance;
        while (Vector3.Distance(targetObject.transform.position, targetPost) > 0.01f)
        {
            targetObject.transform.position = Vector3.MoveTowards(targetObject.transform.position, targetPost, moveSpeed * Time.deltaTime);
            yield return null;
        }
        Destroy(targetObject);
    }
}
