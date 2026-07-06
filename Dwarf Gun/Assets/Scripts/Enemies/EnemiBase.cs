using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    [Header("Funcionamiento")]
    public GameObject objective;
    public GameObject player;
    public GameObject bullet;

    [Header("Stats")]
    protected float speed = 0f;
    protected int live = 0;
    protected int moneyOnDeath = 0;

    [Header("Separación")]
    public float separationDistance = 0.8f;  
    public float separationForce = 2f;

    void Update()
    {
        followObjective();
    }

    public virtual void followObjective()
    {
        if (objective != null)
        {
            Debug.Log("Persiguiendo");
            transform.position = Vector2.MoveTowards(transform.position, objective.transform.position, speed * Time.deltaTime);
        }
    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            live -= bullet.GetComponent<BasicBullet>().bulletDamage;

            if (live <= 0)
            {
                Destroy(gameObject);
                player.GetComponent<Player>().money += moneyOnDeath;
            }
        }
    }

    public virtual void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            // Vector que apunta lejos del otro enemigo
            Vector2 away = (transform.position - collision.transform.position);

            float dist = away.magnitude;

            if (dist < separationDistance && dist > 0.001f)
            {
                // Dirección de separación
                Vector2 separationDir = away.normalized;

                // Aplicar desplazamiento directo alejándose del otro enemigo
                transform.position += (Vector3)(separationDir * separationForce * Time.deltaTime);
            }
        }
    }

}
