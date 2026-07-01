using UnityEngine;
using System.Collections.Generic;

public class Tower : MonoBehaviour
{
    [Header("Piezas torre")]
    public GameObject towerMidle;
    public GameObject towerShootGun;
    public int level = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            gameObject.tag = "Touchable";
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            gameObject.tag = "null";
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
}
