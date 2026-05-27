using UnityEngine;

public class Bala : MonoBehaviour
{
    public float velocidad = 10f;
    private Vector2 direccion;

    public void Inicializar(Vector2 dir)
    {
        direccion = dir;
    }

    void Update()
    {
        transform.Translate(direccion * velocidad * Time.deltaTime);
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}