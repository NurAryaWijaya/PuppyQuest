using UnityEngine;

public class TrapEnemy1 : MonoBehaviour
{
    public GameObject trap;
    public bool hasTrigger = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            hasTrigger = true;
            trap.SetActive(false);
        }
    }
}
