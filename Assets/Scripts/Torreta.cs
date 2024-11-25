using UnityEngine;

public class Torreta : MonoBehaviour
{
    public GameObject balaPrefab; // Prefab de la bala
    public float velocidadBase = 5.0f; // Velocidad base de la bala
    public float velocidadMaxima = 20.0f; // Velocidad m�xima de la bala
    public float velocidadIncremento = 10.0f; // Incremento de velocidad por segundo de presi�n
    public float velocidadMovimientoTorreta = 2.0f; // Velocidad de la torreta
    public float limiteIzquierdo = -5.0f; // L�mite izquierdo del mapa
    public float limiteDerecho = 5.0f; // L�mite derecho del mapa

    private float tiempoInicioPresion; // Tiempo en que se presion� el click

    void Update()
    {
        // Movimiento de la torreta
        transform.position += Vector3.right * velocidadMovimientoTorreta * Time.deltaTime;

        // Cambiar direcci�n al alcanzar los l�mites
        if (transform.position.x > limiteDerecho || transform.position.x < limiteIzquierdo)
        {
            velocidadMovimientoTorreta = -velocidadMovimientoTorreta; // Cambia la direcci�n
        }

        // Detectar si se presiona el bot�n izquierdo del mouse
        if (Input.GetMouseButtonDown(0)) // Comienza a medir el tiempo
        {
            tiempoInicioPresion = Time.time;
        }

        // Detectar si se suelta el bot�n izquierdo del mouse
        if (Input.GetMouseButtonUp(0)) // Dispara la bala
        {
            float tiempoPresionado = Time.time - tiempoInicioPresion;
            Disparar(tiempoPresionado);
        }
    }

    void Disparar(float tiempoPresionado)
    {
        // Calcular la velocidad de la bala seg�n el tiempo de presi�n
        float velocidadBala = Mathf.Clamp(
            velocidadBase + tiempoPresionado * velocidadIncremento,
            velocidadBase,
            velocidadMaxima
        );

        // Generar una bala desde la posici�n de la torreta
        GameObject nuevaBala = Instantiate(balaPrefab, transform.position, Quaternion.identity);
        if (nuevaBala != null)
        {
            // Asignar la velocidad calculada a la bala
            Bala scriptBala = nuevaBala.GetComponent<Bala>();
            if (scriptBala != null)
            {
                scriptBala.velocidad = velocidadBala;
            }

            nuevaBala.SetActive(true); // Activar la bala
        }
    }
}


