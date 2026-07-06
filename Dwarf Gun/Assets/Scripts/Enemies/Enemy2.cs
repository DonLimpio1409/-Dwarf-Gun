using UnityEngine;

public class Enemy : EnemyBase
{
    [Header("Stats")]
    [SerializeField] private float speed2 = 0f;
    [SerializeField] private int live2 = 0;
    [SerializeField] private int moneyOnDeath2 = 0;

    void Start()
    {
        speed = speed2;
        live = live2;
        moneyOnDeath = moneyOnDeath2;
    }

}
