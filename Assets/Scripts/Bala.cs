using UnityEngine;

public class Bala : MonoBehaviour
{
    public float velocidad = 5.0f; // Velocidad de la bala
    public float tiempoDeVida = 3.0f; // Tiempo m�ximo antes de desactivarse

    private float tiempoActivada;

    void OnEnable()
    {
        tiempoActivada = Time.time; // Registrar el tiempo en que se activa la bala
    }

    void Update()
    {
        // Mover la bala hacia arriba (en el eje Y)
        transform.position += Vector3.up * velocidad * Time.deltaTime;

        // Desactivar la bala si excede su tiempo de vida
        if (Time.time - tiempoActivada >= tiempoDeVida)
        {
            gameObject.SetActive(false); // En lugar de destruir la bala, la desactivamos
        }
    }
}


