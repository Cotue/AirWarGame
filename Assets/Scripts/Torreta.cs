using UnityEngine;

public class Torreta : MonoBehaviour
{
    public float velocidad = 2.0f; // Velocidad de movimiento
    public float limiteIzquierdo = -5.0f; // Límite izquierdo del mapa
    public float limiteDerecho = 5.0f; // Límite derecho del mapa
    public GameObject balaPrefab; // Prefab de la bala
    public float tiempoEntreDisparos = 1.0f; // Tiempo entre disparos

    private float tiempoUltimoDisparo = 0.0f;

    void Update()
    {
        // Movimiento de izquierda a derecha
        transform.position += Vector3.right * velocidad * Time.deltaTime;

        // Cambiar dirección al alcanzar los límites
        if (transform.position.x > limiteDerecho || transform.position.x < limiteIzquierdo)
        {
            velocidad = -velocidad; // Cambia de dirección
        }

        // Disparar balas
        if (Time.time - tiempoUltimoDisparo >= tiempoEntreDisparos)
        {
            Disparar();
            tiempoUltimoDisparo = Time.time;
        }
    }

    void Disparar()
    {
        // Generar una bala desde la posición de la torreta
        GameObject nuevaBala = BalaPool.Instancia.ObtenerBala();
        if (nuevaBala != null)
        {
            nuevaBala.transform.position = transform.position;
            nuevaBala.SetActive(true);
        }
    }
}
