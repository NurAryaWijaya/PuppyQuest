using UnityEngine;

public class TrapTriger3 : MonoBehaviour
{
    public Trap3 trapController;
    public int stage = 1; // 1 untuk trigger pertama, 2 untuk kedua

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        if (stage == 1)
        {
            trapController.MovePlatformDown(4f, false);
        }
        else if (stage == 2)
        {
            trapController.MovePlatformDown(10f, true);
        }

        // Optional: Nonaktifkan trigger ini agar tidak retrigger
        GetComponent<Collider2D>().enabled = false;
    }
}
