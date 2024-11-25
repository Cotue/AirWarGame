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
    /*
    void OnTriggerEnter2D(Collider2D other) // Cambia a OnTriggerEnter si es 3D
    {
        if (other.CompareTag("Avion")) // Aseg�rate de que el avi�n tenga el tag "Avion"
        {
            Debug.Log("�Colisi�n detectada con un avi�n!");
            Destroy(other.gameObject); // Destruye el avi�n
            gameObject.SetActive(false); // Desactiva la bala
        }
    }
    */
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Avion")) // Aseg�rate de que el avi�n tenga el tag "Avion"
        {
            Debug.Log("�Colisi�n detectada con un avi�n!");
            Avion avion = other.GetComponent<Avion>();
            if (avion != null)
            {
                GameManager.Instance.MarcarAvionComoDerribado(avion);
            }
            Destroy(other.gameObject); // Destruye el avi�n
            // Desactiva la bala despu�s del impacto
            gameObject.SetActive(false);
        }
    }

}


