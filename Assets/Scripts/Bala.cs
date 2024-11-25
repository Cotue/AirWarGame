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
        // Mover la bala hacia arriba (en el eje Y)
        transform.position += Vector3.up * velocidad * Time.deltaTime;

        // Desactivar la bala si excede su tiempo de vida
        if (Time.time - tiempoActivada >= tiempoDeVida)
        {
            gameObject.SetActive(false); // En lugar de destruir la bala, la desactivamos
        }
    }
    /*
    void OnTriggerEnter2D(Collider2D other) // Cambia a OnTriggerEnter si es 3D
    {
        if (other.CompareTag("Avion")) // Asegúrate de que el avión tenga el tag "Avion"
        {
            Debug.Log("¡Colisión detectada con un avión!");
            Destroy(other.gameObject); // Destruye el avión
            gameObject.SetActive(false); // Desactiva la bala
        }
    }
    */
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Avion")) // Asegúrate de que el avión tenga el tag "Avion"
        {
            Debug.Log("¡Colisión detectada con un avión!");
            Avion avion = other.GetComponent<Avion>();
            if (avion != null)
            {
                GameManager.Instance.MarcarAvionComoDerribado(avion);
            }
            Destroy(other.gameObject); // Destruye el avión
            // Desactiva la bala después del impacto
            gameObject.SetActive(false);
        }
    }

}


