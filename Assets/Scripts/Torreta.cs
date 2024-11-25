using UnityEngine;

public class Torreta : MonoBehaviour
{
    public GameObject balaPrefab; // Prefab de la bala
    public float velocidad = 2.0f; // Velocidad de movimiento
    public float limiteIzquierdo = -5.0f; // Límite izquierdo del mapa
    public float limiteDerecho = 5.0f; // Límite derecho del mapa

    void Update()
    {
        // Movimiento de la torreta
        transform.position += Vector3.right * velocidad * Time.deltaTime;

        // Cambiar dirección al alcanzar los límites
        if (transform.position.x > limiteDerecho || transform.position.x < limiteIzquierdo)
        {
            velocidad = -velocidad; // Cambia la dirección
        }

        // Detectar si se presiona el botón izquierdo del mouse
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Click izquierdo detectado");
            Disparar();
        }
    }

    void Disparar()
    {
        // Generar una bala desde la posición de la torreta
        GameObject nuevaBala = BalaPool.Instancia.ObtenerBala();
        if (nuevaBala != null)
        {
            nuevaBala.transform.position = transform.position; // Asignar la posición de disparo
            nuevaBala.SetActive(true); // Activar la bala
        }
    }
}

