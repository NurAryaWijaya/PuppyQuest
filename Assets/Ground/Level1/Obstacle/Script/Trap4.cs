using System.Security.Cryptography;
using UnityEngine;

public class Trap4 : MonoBehaviour
{
    public GameObject targetPrefab;
    public float movedistance = 3f;
    public float moveSpeed = 2f;

    public bool hasTrigger = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!hasTrigger && collision.CompareTag("Player"))
        {
            hasTrigger = true;
            StartCoroutine(StartMove());
            Debug.Log("Berhasil Triger");
        }
    }

    private System.Collections.IEnumerator StartMove()
    {
        Vector3 startPos = targetPrefab.transform.position;
        Vector3 targetPos = startPos + Vector3.right * movedistance;
        while(Vector3.Distance(targetPrefab.transform.position, targetPos) > 0.01f)
        {
            targetPrefab.transform.position = Vector3.MoveTowards(targetPrefab.transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }
        Debug.Log("Berhasil bergeser");
    }
}
