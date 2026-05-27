using UnityEngine;
using UnityEngine.InputSystem;

public class MovimientoSara : MonoBehaviour
{
    public float velocidad = 3f;
    public float fuerzaSalto = 10f;
    public GameObject balaPrefab;
    public Transform puntoDisparo;
    private Animator animator;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private SpriteRenderer puntoDisparoSR;
    private Vector2 direccionDisparo = Vector2.right;
    private bool estaEnSuelo = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        puntoDisparoSR = puntoDisparo.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        float horizontal = 0f;

        if (Keyboard.current.rightArrowKey.isPressed || Keyboard.current.dKey.isPressed)
            horizontal = 1f;
        else if (Keyboard.current.leftArrowKey.isPressed || Keyboard.current.aKey.isPressed)
            horizontal = -1f;

        rb.linearVelocity = new Vector2(horizontal * velocidad, rb.linearVelocity.y);

        if (horizontal > 0)
        {
            spriteRenderer.flipX = false;
            puntoDisparoSR.flipX = false;
            puntoDisparo.localPosition = new Vector3(Mathf.Abs(puntoDisparo.localPosition.x), puntoDisparo.localPosition.y, 0);
            direccionDisparo = Vector2.right;
        }
        else if (horizontal < 0)
        {
            spriteRenderer.flipX = true;
            puntoDisparoSR.flipX = true;
            puntoDisparo.localPosition = new Vector3(-Mathf.Abs(puntoDisparo.localPosition.x), puntoDisparo.localPosition.y, 0);
            direccionDisparo = Vector2.left;
        }

        if (horizontal != 0)
            animator.SetBool("Caminando", true);
        else
            animator.SetBool("Caminando", false);

        if ((Keyboard.current.upArrowKey.wasPressedThisFrame || Keyboard.current.wKey.wasPressedThisFrame) && estaEnSuelo)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, fuerzaSalto);
            animator.SetTrigger("Saltando");
        }

        if (Keyboard.current.spaceKey.wasPressedThisFrame)
            Disparar();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        estaEnSuelo = true;
        
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        estaEnSuelo = false;
    }

    void Disparar()
    {
        float offsetX = puntoDisparo.position.x - transform.position.x;
        Vector3 posicion = new Vector3(
            transform.position.x + (direccionDisparo == Vector2.left ? -Mathf.Abs(offsetX) : Mathf.Abs(offsetX)),
            puntoDisparo.position.y, 0);

        GameObject nuevaBala = Instantiate(balaPrefab, posicion, Quaternion.identity);

        SpriteRenderer balaSR = nuevaBala.GetComponent<SpriteRenderer>();
        if (direccionDisparo == Vector2.left)
            balaSR.flipX = true;

        nuevaBala.GetComponent<Bala>().Inicializar(direccionDisparo);
    }
}