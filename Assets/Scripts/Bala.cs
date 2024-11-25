using UnityEngine;

public class Bala : MonoBehaviour
{
    public float velocidad = 5.0f; // Velocidad de la bala
    public float tiempoDeVida = 3.0f; // Tiempo máximo antes de desactivarse

    private float tiempoActivada;

    void OnEnable()
    {
        tiempoActivada = Time.time; // Registrar el tiempo en que se activa la bala
    }

    void Update()
    {
        // Mover la bala hacia arriba
        transform.position += Vector3.up * velocidad * Time.deltaTime;

        // Desactivar la bala si excede su tiempo de vida
        if (Time.time - tiempoActivada >= tiempoDeVida)
        {
            DesactivarBala(); // Usar el método para desactivar la bala
        }
    }

    void DesactivarBala()
    {
        gameObject.SetActive(false); // En lugar de destruir la bala, la desactivamos
    }
}

