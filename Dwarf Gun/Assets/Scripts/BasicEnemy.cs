using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    [Header("Funcionamiento")]
    public GameObject objective;
    public GameObject basicBullet;

    [Header("Stats")]
    float speed = 3f;
    [SerializeField] int live = 3;

    [Header("Separación")]
    public float separationDistance = 0.8f;   // Distancia mínima deseada
    public float separationForce = 2f;        // Fuerza de separación

    void Update()
    {
        followObjective();
    }

    private void followObjective()
    {
        if (objective != null)
        {
            transform.position = Vector2.MoveTowards(
                transform.position,
                objective.transform.position,
                speed * Time.deltaTime
            );
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            if(collision.gameObject == basicBullet)
            {
                live -= basicBullet.GetComponent<BasicBullet>().bulletDamage;
            }

            if (live <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
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
