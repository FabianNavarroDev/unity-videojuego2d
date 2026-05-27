using UnityEngine;
using UnityEngine.InputSystem;

public class MovimientoSara : MonoBehaviour
{
    public float velocidad = 3f;
    private Animator animator;
    private Rigidbody2D rb;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
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

        if (movimiento != Vector2.zero)
            animator.SetBool("Caminando", true);
        else
            animator.SetBool("Caminando", false);
    }
}