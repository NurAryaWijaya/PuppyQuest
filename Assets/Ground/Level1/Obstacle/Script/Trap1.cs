using System;
using UnityEngine;

public class Trap1 : MonoBehaviour
{
    public GameObject targetPrefab;
    public float fallSpeed = 2f; // Semakin kecil, semakin pelan

    private bool isFalling = false;
    private Camera mainCam;

    private void Start()
    {
        mainCam = Camera.main;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isFalling && other.CompareTag("Player"))
        {
            isFalling = true;
            StartCoroutine(FallOutOfView());
            Debug.Log("Trigger jatuh aktif");
        }
    }

    private System.Collections.IEnumerator FallOutOfView()
    {
        while (IsVisible(targetPrefab))
        {
            targetPrefab.transform.position += Vector3.down * fallSpeed * Time.deltaTime;
            yield return null;
        }

        Debug.Log("Prefab sudah keluar layar, dihancurkan");
        Destroy(targetPrefab); // atau targetPrefab.SetActive(false);
    }

    private bool IsVisible(GameObject obj)
    {
        Vector3 screenPos = mainCam.WorldToViewportPoint(obj.transform.position);
        return screenPos.y > 0; // Selama masih di atas layar (0 = bawah layar)
    }
}
