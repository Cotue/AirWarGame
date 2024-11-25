using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    public Text relojTexto; // Referencia al componente Text del reloj
    public float tiempoLimite = 60f; // Límite de tiempo en segundos

    private float tiempoInicio;

    void Start()
    {
        tiempoInicio = Time.time;
    }

    void Update()
    {
        // Calcular el tiempo transcurrido
        float tiempoTranscurrido = Time.time - tiempoInicio;

        // Formatear el tiempo en minutos y segundos
        int minutos = Mathf.FloorToInt(tiempoTranscurrido / 60);
        int segundos = Mathf.FloorToInt(tiempoTranscurrido % 60);

        // Actualizar el texto del reloj
        relojTexto.text = $"{minutos:00}:{segundos:00}";

        // Verificar si se alcanzó el límite de tiempo
        if (tiempoTranscurrido >= tiempoLimite)
        {
            TerminarJuego();
        }
    }

    void TerminarJuego()
    {
        // Detener el tiempo del juego
        Time.timeScale = 0;

        // Mostrar mensaje de fin de juego
        Debug.Log("¡El tiempo se ha agotado! Fin del juego.");

        // Opcional: Mostrar un mensaje en pantalla usando UI
        if (relojTexto != null)
        {
            relojTexto.text = "¡Tiempo finalizado!";
        }
    }
}

