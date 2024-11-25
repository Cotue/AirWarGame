using UnityEngine;

public class Torreta : MonoBehaviour
{
    public GameObject balaPrefab; // Prefab de la bala
    public float velocidad = 2.0f; // Velocidad de movimiento
    public float limiteIzquierdo = -5.0f; // L�mite izquierdo del mapa
    public float limiteDerecho = 5.0f; // L�mite derecho del mapa

    void Update()
    {
        // Movimiento de la torreta
        transform.position += Vector3.right * velocidad * Time.deltaTime;

        // Cambiar direcci�n al alcanzar los l�mites
        if (transform.position.x > limiteDerecho || transform.position.x < limiteIzquierdo)
        {
            velocidad = -velocidad; // Cambia la direcci�n
        }

        // Detectar si se presiona el bot�n izquierdo del mouse
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Click izquierdo detectado");
            Disparar();
        }
    }

    void Disparar()
    {
        // Generar una bala desde la posici�n de la torreta
        GameObject nuevaBala = BalaPool.Instancia.ObtenerBala();
        if (nuevaBala != null)
        {
            nuevaBala.transform.position = transform.position; // Asignar la posici�n de disparo
            nuevaBala.SetActive(true); // Activar la bala
        }
    }
}

