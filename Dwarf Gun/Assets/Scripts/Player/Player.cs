using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Header("Input Actions")]
    public InputActionReference moveAction;
    public InputActionReference interactAction;
    public InputActionReference buildAction;
    public InputActionReference shootAction;

    [Header("Movimiento")]
    private Vector2 move;
    private Rigidbody2D rb;

    [Header("Interactuar")]
    public GameObject towerPrefab;
    public GameObject towerGenerate;
    public LayerMask interact;

    [Header("Disparo")]
    public GameObject bulletPrefab;
    public GameObject gun;
    float cooldownShot = 1f;

    [Header("Stats")]
    public int live = 3;
    public int money = 0;
    [SerializeField] private float moveSpeed = 5f;



    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        Move();
        Interact();
        Built();
        MoveGun();
        Shot();
    }

    void FixedUpdate()
    {
        //Aplicar movimiento
        rb.MovePosition(rb.position + move * moveSpeed * Time.fixedDeltaTime);
    }

    private void Move()
    {
        //Leer Accion de movimiento
        move = moveAction.action.ReadValue<Vector2>();
        if(move.x > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }else if (move.x < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }

    }

    private void Interact()
    {
        //Leer interactuacion y aplicar logica
        if (interactAction.action.triggered)
        {
            Vector2 mousePos = Mouse.current.position.ReadValue();
            Vector2 worldPos = Camera.main.ScreenToWorldPoint(mousePos);

            Collider2D hit = Physics2D.OverlapPoint(worldPos, interact);

            if (hit != null && hit.CompareTag("TouchableTower"))
            {
                hit.transform.parent.GetComponent<Tower>().MakeIt();
            }
            else
            {
                //Animacion martillo
                Debug.Log("PITO");
            }
        }
    }

    private void Built()
    {
        //Leer interaccion contruir
        if(buildAction.action.triggered)
        {
            //Instanciar torre
            Instantiate(towerPrefab, towerGenerate.transform.position, Quaternion.identity);
        }
    }

    private void MoveGun()
    {
        //Obtener posición del ratón en pantalla
        Vector2 mouseScreenPos = Mouse.current.position.ReadValue();
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);

        Vector2 direction = mouseWorldPos - gun.transform.position;

        //Calcular ángulo en grados
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        gun.transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void Shot()
    {
        cooldownShot -= Time.deltaTime;
        if(shootAction.action.IsPressed() && cooldownShot <= 0f)
        {
            Instantiate(bulletPrefab, gun.transform.position, gun.transform.rotation);
            cooldownShot = 1f;
        }
    }
}
