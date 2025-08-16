using UnityEngine;

public class Trap2 : MonoBehaviour
{
    public GameObject objectToShoot;
    public float moveDistance = 10f;
    public float moveSpeed = 5f;

    private bool hasTrigger = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!hasTrigger && collision.CompareTag("Player"))
        {
            hasTrigger = true;
            StartCoroutine(ShootUp());
        }
    }

    private System.Collections.IEnumerator ShootUp()
    {
        Vector3 startPos = objectToShoot.transform.position;
        Vector3 targetPos = startPos + Vector3.up * moveDistance;
        while(Vector3.Distance(objectToShoot.transform.position, targetPos) > 0.01f)
        {
            objectToShoot.transform.position = Vector3.MoveTowards(objectToShoot.transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }
        Destroy(objectToShoot);
    }
}
