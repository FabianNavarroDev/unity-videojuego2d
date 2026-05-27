using UnityEngine;

public class Moneda : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GestorMonedas.instancia.AgregarMoneda();
            Destroy(gameObject);
        }
    }
}