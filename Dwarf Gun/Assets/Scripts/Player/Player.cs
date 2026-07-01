using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Header("Input Actions")]
    public InputActionReference moveAction;
    public InputActionReference interactAction;
    public InputActionReference buildAction;

    [Header("Movimiento")]
    private Vector2 move;
    [SerializeField] private float moveSpeed = 5f;
    private Rigidbody2D rb;

    [Header("Interactuar")]
    public GameObject towerPrefab;
    public GameObject towerGenerate;
    public LayerMask interact;


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();   
    }
    void Update()
    {
        Move();
        Interact();
        Built();
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

            if (hit != null && hit.CompareTag("Touchable"))
            {
                hit.GetComponent<Tower>().MakeIt();
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

}
