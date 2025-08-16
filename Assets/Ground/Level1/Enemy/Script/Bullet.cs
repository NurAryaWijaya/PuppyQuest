using UnityEngine;
using UnityEngine.UIElements;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public float speed = 5f;
    public float maxDistance = 10f;
    private Vector3 spawnPosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);

        float distance = Vector3.Distance(spawnPosition, transform.position);
        if (distance >= maxDistance)
        {
            Destroy(gameObject);
        }
    }
}
