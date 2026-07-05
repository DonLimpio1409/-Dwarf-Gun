using UnityEngine;
using System.Collections.Generic;

public class Tower : MonoBehaviour
{
    [Header("Deteccion de enemigos")]
    List<GameObject> enemiesInRange = new List<GameObject>();
    public int enemyCount;

    [Header("Disparo")]
    public GameObject bulletPrefab;
    public bool enemyIn;
    public float cooldownShot = 1f;

    [Header("Piezas torre")]
    public GameObject towerMidle;
    public GameObject towerShootGun;
    public GameObject touchable;
    private int level = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            touchable.tag = "TouchableTower";
        }

        if(collision.CompareTag("Enemy"))
        {
            //Agregar enemigo a la lista de enemigos en rango
            enemiesInRange.Add(collision.gameObject);
            enemyIn = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            touchable.tag = "null";
        }

        if(collision.CompareTag("Enemy"))
        {
            //Remover enemigo de la lista de enemigos en rango
            enemiesInRange.Remove(collision.gameObject);
            if(enemiesInRange.Count == 0)
            {
                enemyIn = false;
            }
        }
    }


    public void MakeIt()
    {
        Debug.Log("MakeIt called");
        level++;
        towerMidle.SetActive(true);
        
        if(level == 2)
        {
            towerShootGun.SetActive(true);
        }
    }
    void Update()
    {
        if(level < 2)
        {
            return;
        }
        LookAtEnemy();
        shot();
        enemyCount = enemiesInRange.Count;
    }

    private void LookAtEnemy()
    {
        if (enemiesInRange.Count == 0)
            return;

        Transform nearest = enemiesInRange[0].transform;
        float bestDist = (nearest.position - transform.position).sqrMagnitude;

        for (int i = 1; i < enemiesInRange.Count; i++)
        {
            float d = (enemiesInRange[i].transform.position - transform.position).sqrMagnitude;

            if (d < bestDist)
            {
                bestDist = d;
                nearest = enemiesInRange[i].transform;
            }
        }

        // Mantener altura para evitar que el sprite se incline
        Vector3 lookPos = nearest.position;

        Vector3 dir = lookPos - towerShootGun.transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        towerShootGun.transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void shot()
    {
        cooldownShot -= Time.deltaTime;

        if (enemyIn && cooldownShot <= 0f)
        {
            GameObject bulletFly = Instantiate(bulletPrefab, towerShootGun.transform.position, towerShootGun.transform.rotation);
            cooldownShot = 1f; // Reiniciar el tiempo de espera
            Debug.Log("Bullet instantiated");
        }
    }
}
