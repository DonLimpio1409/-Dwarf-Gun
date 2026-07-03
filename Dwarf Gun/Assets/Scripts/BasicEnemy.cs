using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    public GameObject objective;
    float speed = 3f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(objective != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, objective.transform.position, speed * Time.deltaTime);
        }
    }
}
