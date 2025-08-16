using UnityEngine;

public class Enemy1 : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public TrapEnemy1 trap;

    void Start()
    {
        StartCoroutine(ShootingRoutine());
    }

    private System.Collections.IEnumerator ShootingRoutine()
    {
        while (true)
        {
            float delay = !trap.hasTrigger ? 1.5f : 0.5f;
            Shoot();
            yield return new WaitForSeconds(delay);
        }
    }
    void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}
