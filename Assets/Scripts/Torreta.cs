using UnityEngine;

public class Torreta : MonoBehaviour
{
    public float velocidad = 2.0f; // Velocidad de movimiento
    public float limiteIzquierdo = -5.0f; // L�mite izquierdo del mapa
    public float limiteDerecho = 5.0f; // L�mite derecho del mapa
    public GameObject balaPrefab; // Prefab de la bala
    public float tiempoEntreDisparos = 1.0f; // Tiempo entre disparos

    private float tiempoUltimoDisparo = 0.0f;

    void Update()
    {
        // Movimiento de izquierda a derecha
        transform.position += Vector3.right * velocidad * Time.deltaTime;

        // Cambiar direcci�n al alcanzar los l�mites
        if (transform.position.x > limiteDerecho || transform.position.x < limiteIzquierdo)
        {
            velocidad = -velocidad; // Cambia de direcci�n
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
        // Generar una bala desde la posici�n de la torreta
        GameObject nuevaBala = BalaPool.Instancia.ObtenerBala();
        if (nuevaBala != null)
        {
            nuevaBala.transform.position = transform.position;
            nuevaBala.SetActive(true);
        }
    }
}
