using UnityEngine;

public class Trap5 : MonoBehaviour
{
    public GameObject targetObject;
    public Trap4 trap4;


    private void Start()
    {
        if(targetObject != null)
        {
            targetObject.SetActive(false);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {       
        if (collision.CompareTag("Player") && trap4.hasTrigger)
        {
            if (targetObject != null)
            {
                targetObject.SetActive(true);
                Debug.Log("Berhasil triger trap 5");
            }
        }
    }

}
