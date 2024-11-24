using UnityEngine;

public class Bala : MonoBehaviour
{
    public float velocidad = 5.0f; // Velocidad de la bala
    public float tiempoDeVida = 3.0f; // Tiempo máximo antes de desactivarse

    private float tiempoActivada;

    void OnEnable()
    {
        tiempoActivada = Time.time;
    }

    void Update()
    {
        // Mover la bala hacia arriba
        transform.position += Vector3.up * velocidad * Time.deltaTime;

        // Desactivar la bala si excede su tiempo de vida
        if (Time.time - tiempoActivada >= tiempoDeVida)
        {
            BalaPool.Instancia.DevolverBala(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Si la bala colisiona con algo, regresa al pool
        BalaPool.Instancia.DevolverBala(gameObject);
    }
}
