using UnityEngine;

public class BasicBullet : MonoBehaviour
{
    float liveTime = 10f;
    float speed = 10f;
    public int bulletDamage = 1;
    // Update is called once per frame
    void Update()
    {
        liveTime -= Time.deltaTime;

        gameObject.transform.Translate(Vector2.right * speed * Time.deltaTime);
        liveTime -= Time.deltaTime;

        if (liveTime <= 0f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}
