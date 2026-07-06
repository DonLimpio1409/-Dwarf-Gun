using UnityEngine;

public class Enemy1 : EnemyBase
{
    [Header("Stats")]
    [SerializeField] private float speed1 = 0f;
    [SerializeField] private int live1 = 0;
    [SerializeField] private int moneyOnDeath1 = 0;

    void Start()
    {
        speed = speed1;
        live = live1;
        moneyOnDeath = moneyOnDeath1;
    }
}
