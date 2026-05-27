using UnityEngine;
using UnityEngine.InputSystem;

public class MovimientoSara : MonoBehaviour
{
    public float velocidad = 3f;
    public GameObject balaPrefab;
    public Transform puntoDisparo;
    private Animator animator;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Vector2 direccionDisparo = Vector2.right;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        float horizontal = 0f;
        float vertical = 0f;

        if (Keyboard.current.rightArrowKey.isPressed || Keyboard.current.dKey.isPressed)
            horizontal = 1f;
        else if (Keyboard.current.leftArrowKey.isPressed || Keyboard.current.aKey.isPressed)
            horizontal = -1f;

        if (Keyboard.current.upArrowKey.isPressed || Keyboard.current.wKey.isPressed)
            vertical = 1f;
        else if (Keyboard.current.downArrowKey.isPressed || Keyboard.current.sKey.isPressed)
            vertical = -1f;

        Vector2 movimiento = new Vector2(horizontal, vertical);
        rb.linearVelocity = movimiento * velocidad;

        if (horizontal > 0)
        {
            spriteRenderer.flipX = false;
            direccionDisparo = Vector2.right;
        }
        else if (horizontal < 0)
        {
            spriteRenderer.flipX = true;
            direccionDisparo = Vector2.left;
        }

        if (movimiento != Vector2.zero)
            animator.SetBool("Caminando", true);
        else
            animator.SetBool("Caminando", false);

        if (Keyboard.current.spaceKey.wasPressedThisFrame)
            Disparar();
    }

    void Disparar()
{
    // Espejear posición del punto de disparo según dirección
    Vector3 posicion = puntoDisparo.position;
    if (direccionDisparo == Vector2.left)
        posicion = new Vector3(transform.position.x - (puntoDisparo.localPosition.x), 
                               puntoDisparo.position.y, 0);

    GameObject nuevaBala = Instantiate(balaPrefab, posicion, Quaternion.identity);
    
    // Espejear sprite de la bala
    SpriteRenderer balaSR = nuevaBala.GetComponent<SpriteRenderer>();
    if (direccionDisparo == Vector2.left)
        balaSR.flipX = true;

    nuevaBala.GetComponent<Bala>().Inicializar(direccionDisparo);
}
}